using Innovation.Areas.Innovation.Helpers;
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
using Innovation.Areas.Innovation.Models;
using Innovation.Areas.Innovation.Models.Components;


namespace Innovation.Areas.Innovation.Mappers.Components
{
    public interface IContactInformationService
    {
        ContactInformation GetContactInfoData();
    }
    public class ContactInformationService : IContactInformationService
    {
        public ContactInformation GetContactInfoData()
        {
            ContactInformation contactInformation = new ContactInformation();
            Item Datasource = SitecoreAccess.GetDataSourceItem();
            if (Datasource != null)
            {
                contactInformation.ContactsInformation = new HtmlString(FieldRenderer.Render(Datasource, "Contact Us Information"));
            }
            return contactInformation;
        }
    }
}