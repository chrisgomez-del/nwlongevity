using NM_MultiSites.Areas.Innovation.Helpers;
using NM_MultiSites.Areas.Innovation.Mappers.Forms;
using NM_MultiSites.Areas.Innovation.Models.Forms;
using NM_MultiSites.Areas.Innovation.Infrastructure.Action;
using Newtonsoft.Json;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NM_MultiSites.Areas.Innovation.Controllers
{
    public class CustomFormsController : Controller
    {
        private readonly IContactUsFormService _contactUsFormService;

        public CustomFormsController()
        {
            _contactUsFormService = new ContactUsFormService();
        }
        
        public ActionResult PartnershipIntakeForm()
        {
            PartnershipIntakeForm intakeForm = new PartnershipIntakeForm();
            return View(intakeForm);
        }

        [HttpPost]
        public ActionResult PartnershipIntakeForm(PartnershipIntakeForm model)
        {
            var formMessages = _contactUsFormService.GetContactFormData();
            model.ConfirmationMessage = formMessages.FailureMessage;
            if (!model.Robotest.IsNullOrEmpty())
            {
                ModelState.AddModelError("Robotest", "Robot detected!");
            }

            if (ModelState.IsValid)
            {

               var subject = !formMessages.Subject.IsNullOrEmpty() ? formMessages.Subject : "Patnership Intake Form Submission";
               string bodyHTML = ReplaceTokensIntakeForm(formMessages, model);

                try
                {
                    Email.SendEMail(formMessages.Sender, formMessages.Recipients, subject, bodyHTML,model.CompanyPitch);
                }
                catch (Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Innovation contact us error sending email", ex, "Innovation");
                    return new TransferToJsonResult(JsonConvert.SerializeObject(new { result = formMessages.EmailSendingError, success = false })); 
                }
                return new TransferToJsonResult(JsonConvert.SerializeObject(new { result = formMessages.SuccessMessage, success = true }));
            }
            return new TransferToJsonResult(JsonConvert.SerializeObject(new { result = formMessages.FailureMessage, success = false }));
        }

        // GET: Innovation/Forms
        public ActionResult ContactUsForm()
        {
            ContactUsForm ContactUs = new ContactUsForm();
            return View(ContactUs);
        }

        // POST: Innovation/Forms
        [HttpPost]
        public ActionResult ContactUsForm(ContactUsForm model)
        {
            var formMessages = _contactUsFormService.GetContactFormData();
            model.ConfirmationMessage = formMessages.FailureMessage;
            if (!model.Robotest.IsNullOrEmpty())
            {
                ModelState.AddModelError("Robotest", "Robot detected!");
            }

            if (ModelState.IsValid)
            {
                
                var subject = !formMessages.Subject.IsNullOrEmpty() ? formMessages.Subject : "Contact Us Form Submission";
                string bodyHTML = ReplaceTokens(formMessages, model);

                try
                {
                    Email.SendEMail(formMessages.Sender, formMessages.Recipients, subject, bodyHTML);
                }
                catch(Exception ex)
                {
                    Sitecore.Diagnostics.Log.Error("Innovation contact us error sending email", ex, "Innovation");
                    return new TransferToJsonResult(JsonConvert.SerializeObject(new { result = formMessages.EmailSendingError, success = false }));
                }
                model.ConfirmationMessage = formMessages.SuccessMessage;
                return new TransferToJsonResult(JsonConvert.SerializeObject(new { result = formMessages.SuccessMessage, success = true }));
            }
            return new TransferToJsonResult(JsonConvert.SerializeObject(new { result = formMessages.FailureMessage, success = false }));
        }
        private string ReplaceTokens(FormMessages msg,ContactUsForm form)
        {
            var values = new Dictionary<string, string> {
                { "{{name}}",form.FullName },
                { "{{email}}", form.EmailAddress },
                { "{{msg}}", form.FormMessage }
        };
            string message = msg.Body.ToString();
            foreach (var key in values.Keys)
            {
                message = message.Replace(key, values[key]);
            }
            return message;
        }

        private string ReplaceTokensIntakeForm(FormMessages msg, PartnershipIntakeForm form)
        {
            var values = new Dictionary<string, string> {
                { "{{name}}",form.FullName },
                { "{{email}}", form.EmailAddress },
                { "{{phone}}", form.PhoneNumber },
                { "{{companyName}}", form.CompanyName},
                { "{{title}}",form.Title },
                { "{{companyURL}}",form.CompanyURL },
                { "{{idea}}", form.BusinessIdea },
                { "{{priority}}", form.Priority },
                { "{{solution}}", form.SolutionMessage},
                { "{{benefits}}",form.BenefitsMessage },
                { "{{ROI}}",form.ROIMessage },
                { "{{diff}}",form.CompetitorsMessage },
                { "{{list}}", form.OthersInfoMessage },
                { "{{partnership}}", form.PartnershipMessage },
                { "{{yesno}}", form.SelectedOption}
        };
            string message = msg.Body.ToString();
            foreach (var key in values.Keys)
            {
                message = message.Replace(key, values[key]);
            }
            return message;
        }
    }
    public class Response
    {
        public bool Successful { get;  set; }
        public string Message { get; set; }

    }
}