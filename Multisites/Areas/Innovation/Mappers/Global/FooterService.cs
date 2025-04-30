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
using NM_MultiSites.Areas.Innovation.Models.Global;

namespace NM_MultiSites.Areas.Innovation.Mappers.Global
{
    public interface IFooterService
    {
        Footer GetFooter();
    }
    public class FooterService : IFooterService
    {
        public Footer GetFooter()
        {
            Footer footer = new Footer();
            Item i = SitecoreAccess.getSiteSettingItem();
            List<Item> MainNavItems = SitecoreAccess.GetMultiListItems(i, "Footer Nav Links");
            List<Item> AdditionalNavItems = SitecoreAccess.GetMultiListItems(i, "Additional Footer Nav Links");
            if (MainNavItems != null && MainNavItems.Any())
            {
                foreach (Item item in MainNavItems)
                {
                    GeneralLink link = new GeneralLink()
                    {
                        Title = new HtmlString(FieldRenderer.Render(item, "Title")),
                        CTALink = String.IsNullOrEmpty(item.Fields["Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(item.Fields["Link"]),
                    };
                    footer.MainNavLinks.Add(link);
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
                    footer.AdditionalNavLinks.Add(link);
                }
            }
            footer.GenericContent = new HtmlString(FieldRenderer.Render(i, "Generic Footer Content"));
            footer.NMImage = SitecoreAccess.GetMediaUrl(i, "NM Logo");
            return footer;
        }
    }
}