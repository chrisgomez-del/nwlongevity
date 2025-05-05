using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Web.UI.WebControls;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using NM_MultiSites.Areas.ems.Helper;
using NM_MultiSites.Areas.ems.Models;
using System.Text;

namespace NM_MultiSites.Areas.ems.Controllers
{
    public class FormsController : Controller
    {
        public ActionResult ContactUs()
        {
            var formitem = Sitecore.Context.Database.GetItem("/sitecore/content/NMPN/settings/Form");
            string key = formitem?.Fields["GoogleRecaptchaSiteKey"]?.Value;
            ContactUsForm form = new ContactUsForm() {
                skey = key
            };
            return View(form);
        }
        [HttpPost]
        public ActionResult ContactUs(ContactUsForm form)
        {
            var formitem = Sitecore.Context.Database.GetItem("/sitecore/content/NMPN/settings/Form");
            string key = formitem?.Fields["GoogleRecaptchaSecretKey"]?.Value;
            string subject = formitem?.Fields["Subject"]?.Value;
            string toaddress = formitem?.Fields["ToAddress"]?.Value;
            var captchaResponse = Request["g-recaptcha-response"];
            bool valid = Captcha.ValidateCaptcha(captchaResponse, key);
            if(!valid)
            {
                ModelState.AddModelError(String.Empty, "Captcha is not valid");
            }
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            try
            {
                Email.SendEMail("no-reply@nm.org", toaddress, subject, GeneratecontactUSEmailBody(form));
                TryUpdateModel(form);
                ModelState.Clear();
                return View("~/Areas/ems/Views/Forms/Success.cshtml");
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message, "SendMail");
                TryUpdateModel(form);
                ModelState.Clear();
                return View("~/Areas/ems/Views/Forms/Error.cshtml");
            }
            
        }


        private string GeneratecontactUSEmailBody(ContactUsForm form)
        {
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>Contact Us Form</h1>");
            mailBody.AppendFormat("Hi,");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Below are the details");
            mailBody.AppendFormat("<p>Name : {0}</p>", form.Name);
            mailBody.AppendFormat("<p>Phone : {0}</p>", form.Phone);
            mailBody.AppendFormat("<p>Email : {0}</p>", form.Email);
            mailBody.AppendFormat("<p>Address : {0} {1} {2} {3} </p>", form.Address, form.City, form.State, form.Zip);
            mailBody.AppendFormat("<p>Practice : {0}</p>", form.Practice);
            mailBody.AppendFormat("<p>Topic : {0}</p>", form.Topic);
            mailBody.AppendFormat("<p>Message : {0}</p>", form.Message);
            return mailBody.ToString();
        }
    }
}