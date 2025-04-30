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
    }
}