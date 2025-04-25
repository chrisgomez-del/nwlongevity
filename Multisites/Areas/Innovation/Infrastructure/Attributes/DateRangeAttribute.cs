using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

// Slightly modified version of:
// https://stackoverflow.com/a/33175234
//
namespace Innovation.Areas.Innovation.Infrastructure.Attributes
{
    public enum DateRangeType
    {
        Value,
        DependentProperty
    }

    [AttributeUsage(AttributeTargets.All | AttributeTargets.Property)]
    public class DateRangeAttribute : ValidationAttribute, IClientValidatable
    {
        private const string UniversalDatePattern = "MM/dd/yyyy";
        private readonly string _minDate;
        private readonly string _maxDate;
        private readonly DateRangeType _dateRangeType;

        public DateRangeAttribute(string minDate, string maxDate) : this(DateRangeType.Value, minDate, maxDate)
        {
        }

        public DateRangeAttribute(DateRangeType dateRangeType, string minDate, string maxDate)
        {
            if (dateRangeType == DateRangeType.Value)
            {
                if (!IsValidDate(minDate))
                {
                    throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Max date should be in {0} format.",
                        UniversalDatePattern));
                }

                if (!IsValidDate(maxDate))
                {
                    throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Min date should be in {0} format.",
                        UniversalDatePattern));
                }
            }

            _dateRangeType = dateRangeType;
            _minDate = minDate;
            _maxDate = maxDate;
        }

        private string MinDate
        {
            get { return _minDate; }
        }

        private string MaxDate
        {
            get { return _maxDate; }
        }

        private DateRangeType DateRangeType
        {
            get { return _dateRangeType; }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            if (metadata != null)
            {
                var controllerModel = ModelMetadataProviders.Current.GetMetadataForProperties(context.Controller.ViewData.Model, metadata.ContainerType);

                if (DateRangeType.Equals(DateRangeType.DependentProperty))
                {
                    DateTime? minDateValue = null;
                    DateTime? maxDateValue = null;

                    if (MinDate != null) {
                        var minDateProperty = controllerModel.FirstOrDefault(p => p.PropertyName == MinDate);
                        if (minDateProperty != null)
                        {
                            minDateValue = (DateTime?)minDateProperty.Model;
                        }
                    }

                    if (MaxDate != null) {
                        
                        var maxDateProperty = controllerModel.FirstOrDefault(p => p.PropertyName == MaxDate);
                        if (maxDateProperty != null)
                        {
                            maxDateValue = (DateTime?)maxDateProperty.Model;
                        }
                    }
                    

                    if (minDateValue.HasValue || maxDateValue.HasValue)
                    {
                        return new[]
                        {
                            new ModelClientValidationDateRangeRule(
                                ErrorMessageString,
                                minDateValue.HasValue ? minDateValue.Value.ToString(UniversalDatePattern) : "",
                                maxDateValue.HasValue ? maxDateValue.Value.ToString(UniversalDatePattern) : "")
                        };
                    }
                    return null;
                }

                return new[]
                {
                    new ModelClientValidationDateRangeRule(
                        ErrorMessageString,
                        MinDate,
                        MaxDate)
                };
            }
            return null;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = ValidationResult.Success;
            var errorResult = new ValidationResult(ErrorMessageString);
            if (value == null)
            {
                return result;
            }

            var dateValue = (DateTime)value;

            if (DateRangeType == DateRangeType.Value)
            {
                if (string.IsNullOrEmpty(MinDate) && string.IsNullOrEmpty(MaxDate))
                {
                    return errorResult;
                }

                var passedMinDate = !string.IsNullOrEmpty(MinDate) ? ParseDate(MinDate) <= dateValue : true;
                var passedMaxDate = !string.IsNullOrEmpty(MaxDate) ? dateValue <= ParseDate(MaxDate) : true;
                if (passedMinDate && passedMaxDate)
                {
                    return result;
                }
            }
            else
            {
                if (validationContext == null || (string.IsNullOrEmpty(MinDate) && string.IsNullOrEmpty(MaxDate)))
                {
                    return errorResult;
                }

                var passedMinDate = false;
                if (!string.IsNullOrEmpty(MinDate)) {
                    var minDatePropertyInfo = validationContext.ObjectType.GetProperty(MinDate);
                    if (minDatePropertyInfo == null) return errorResult;
                    var minDateValue = Convert.ToDateTime(minDatePropertyInfo.GetValue(validationContext.ObjectInstance, null), CultureInfo.CurrentCulture);

                    passedMinDate = minDateValue <= dateValue;
                }
                else
                {
                    passedMinDate = true;
                }

                var passedMaxDate = false;
                if (!string.IsNullOrEmpty(MaxDate)) {
                    var maxDatePropertyInfo = validationContext.ObjectType.GetProperty(MaxDate);
                    if (maxDatePropertyInfo == null) return errorResult;
                    var maxDateValue = Convert.ToDateTime(maxDatePropertyInfo.GetValue(validationContext.ObjectInstance, null), CultureInfo.CurrentCulture);

                    passedMaxDate = dateValue <= maxDateValue;
                }
                else
                {
                    passedMaxDate = true;
                }

                if (passedMinDate && passedMaxDate)
                {
                    return result;
                }
            }

            return errorResult;
        }


        private static DateTime ParseDate(string dateValue)
        {
            return DateTime.ParseExact(
                dateValue, UniversalDatePattern,
                CultureInfo.InvariantCulture);
        }

        private static bool IsValidDate(string dateValue)
        {
            DateTime result;
            DateTime.TryParse(dateValue, out result);

            //TryParse returns MinValue if failure
            return result != DateTime.MinValue;
        }

        private class ModelClientValidationDateRangeRule : ModelClientValidationRule
        {
            public ModelClientValidationDateRangeRule(string errorMessage, string minDateProperty, string maxDateProperty)
            {
                ErrorMessage = errorMessage;
                ValidationType = "daterange";

                if (!string.IsNullOrEmpty(minDateProperty)) {
                    ValidationParameters.Add("mindateproperty", minDateProperty);
                }

                if (!string.IsNullOrEmpty(maxDateProperty)) {
                    ValidationParameters.Add("maxdateproperty", maxDateProperty);
                } 
            }
        }
    }
}
