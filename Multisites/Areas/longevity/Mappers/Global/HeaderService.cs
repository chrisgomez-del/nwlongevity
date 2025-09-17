using NM_MultiSites.Areas.Longevity.Helpers;
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
using NM_MultiSites.Areas.Longevity.Models.Global;
using NM_MultiSites.Areas.Longevity.Models;

namespace NM_MultiSites.Areas.Longevity.Mappers.Global
{
    public interface IHeaderService
    {
        Header GetHeader();
    }
    public class HeaderService : IHeaderService
    {
        public Header GetHeader()
        {
            Header header = new Header();
            Item i = SitecoreAccess.getSiteSettingItem();
            Item currentitem = Sitecore.Context.Item;
            
                header.SourceItem = currentitem;
                header.Title = new HtmlString(FieldRenderer.Render(currentitem, "Navigation Title"));
                header.SubTitle = new HtmlString(FieldRenderer.Render(currentitem, "Description"));
                header.IsHomePageHeader = SitecoreAccess.IsCheckboxChecked(currentitem, "Use Home Page Header");
                header.CaseStudyImage = new HtmlString(FieldRenderer.Render(currentitem, "CaseStudy Header Image"));
                header.AdditionalContent = new HtmlString(FieldRenderer.Render(currentitem, "Additional Content"));

            List<Item> MainNavItems = SitecoreAccess.GetMultiListItems(i, "Main Nav Links");
                List<Item> AdditionalNavItems = SitecoreAccess.GetMultiListItems(i, "Additional Nav Links");
                if (MainNavItems != null && MainNavItems.Any())
                {
                    foreach (Item item in MainNavItems)
                    {
                        GeneralLink link = new GeneralLink()
                        {
                            Title = new HtmlString(FieldRenderer.Render(item, "Title")),
                            CTALink = String.IsNullOrEmpty(item.Fields["Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(item.Fields["Link"]),
                            Class = new HtmlString(FieldRenderer.Render(item, "Class"))
                        };
                        header.MainNavLinks.Add(link);
                    }
                }
                if (AdditionalNavItems != null && AdditionalNavItems.Any())
                {
                    foreach (Item item in AdditionalNavItems)
                    {
                        GeneralLink link = new GeneralLink()
                        {
                            Title = new HtmlString(FieldRenderer.Render(item, "Title")),
                            CTALink = String.IsNullOrEmpty(item.Fields["Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(item.Fields["Link"]),
                        };
                        header.AdditionalNavLinks.Add(link);
                    }
                }
                header.GenericContent = new HtmlString(FieldRenderer.Render(i, "Generic Header Content"));
            
            return header;
        }
    }
}