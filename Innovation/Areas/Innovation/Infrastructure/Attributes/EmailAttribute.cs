using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Innovation.Areas.Innovation.Infrastructure.Attributes
{
    public class EmailAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public char[] Separators { get; set; }
        public EmailAttribute() : base(@"^[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}$")
        { }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return true; //this attribute doesn't make the field required
            var emails = value.ToString().Trim().Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            if (emails.Length > 0)
            {
                var regex = new Regex(Pattern);
                return emails.All(regex.IsMatch);
            }
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[] { new ModelClientValidationRegexRule(FormatErrorMessage(metadata.GetDisplayName()), Pattern) };
        }
    }
}
