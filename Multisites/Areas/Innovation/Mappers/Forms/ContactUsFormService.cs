using NM_MultiSites.Areas.Innovation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using NM_MultiSites.Areas.Innovation.Models;
using NM_MultiSites.Areas.Innovation.Models.Forms;
using NM_MultiSites.Areas.Innovation.API.Youtube.Models;

namespace NM_MultiSites.Areas.Innovation.Mappers.Forms
{
    public interface IContactUsFormService
    {
        FormMessages GetContactFormData();
    }
    public class ContactUsFormService : IContactUsFormService
    {
        public FormMessages GetContactFormData()
        {
            FormMessages formmessages = new FormMessages();
            Item Datasource = SitecoreAccess.GetDataSourceItem();
            if(Datasource!=null)
            {
                string from = Datasource.Fields["Sender"].Value;
                formmessages.Sender = String.IsNullOrWhiteSpace(from) ? "no-reply@nm.org" : from;
                formmessages.Recipients = Datasource.Fields["Recipients"].Value;
                formmessages.Subject = Datasource.Fields["Subject"].Value;
                formmessages.Body = Datasource.Fields["Body"].Value;
                formmessages.SuccessMessage = Datasource.Fields["SuccessMessage"].Value;
                formmessages.FailureMessage = Datasource.Fields["FailureMessage"].Value;
                formmessages.EmailSendingError = Datasource.Fields["EmailSendingError"].Value;

            }
            return formmessages;
        }

        public static SecureSecretSettings GetSecureSecretSettings()
        {
            SecureSecretSettings secret = new SecureSecretSettings();
            Item i = SitecoreAccess.getSiteSettingItem();
            secret.GoogleCaptchaSiteKey = new HtmlString(FieldRenderer.Render(i, "Google Captcha SiteKey"));
            secret.GoogleCaptchaSecretKey = new HtmlString(FieldRenderer.Render(i, "Google Captcha SecretKey"));
            return secret;
        }
    }
}