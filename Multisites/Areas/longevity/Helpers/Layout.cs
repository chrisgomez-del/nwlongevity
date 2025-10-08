using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.longevity.Helpers
{
    public class Layout
    {

        public static string GetHtmlTop()
        {
            var global = SitecoreAccess.getSiteSettingItem();
            return global["HTML Top"];
        }

        public static string GetHtmlBottom()
        {
            var global = SitecoreAccess.getSiteSettingItem();
            return global["HTML Bottom"];
        }
    }
}