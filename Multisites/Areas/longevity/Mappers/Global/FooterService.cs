using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using NM_MultiSites.Areas.Longevity.Models;
using Sitecore.Mvc.Helpers;
using NM_MultiSites.Areas.Longevity.Models.Global;

namespace NM_MultiSites.Areas.Longevity.Mappers.Global
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
            Sitecore.Data.Items.Item currentitem = SitecoreAccess.getSiteSettingItem();

            footer.Title1 = new HtmlString(FieldRenderer.Render(currentitem, "Title 1"));
            footer.Title2 = new HtmlString(FieldRenderer.Render(currentitem, "Title 2"));
            GeneralLink policiesLink = new GeneralLink()
            {
                Title = new HtmlString(SitecoreAccess.LinkTitle(currentitem.Fields["Policies Link"])),
                CTALink = String.IsNullOrEmpty(currentitem.Fields["Policies Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(currentitem.Fields["Policies Link"]),
            };

            footer.PoliciesLink = policiesLink;

            GeneralLink accessiblityLink = new GeneralLink()
            {
                Title = new HtmlString(SitecoreAccess.LinkTitle(currentitem.Fields["Accessibility Link"])),
                CTALink = String.IsNullOrEmpty(currentitem.Fields["Accessibility Link"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(currentitem.Fields["Accessibility Link"]),
            };

            footer.AccessibilityLink = accessiblityLink;

            footer.Copyright = new HtmlString(FieldRenderer.Render(currentitem, "Copyright"));
            footer.Disclaimer = new HtmlString(FieldRenderer.Render(currentitem, "Disclaimer"));

            return footer;
        }
    }
}