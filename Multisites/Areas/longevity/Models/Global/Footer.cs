using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Global
{
    public class Footer
    {
        public Footer()
        {

        }

        public HtmlString Title1 { get; set; }
        public HtmlString Title2 { get; set; }
        public HtmlString FormButtonLabel { get; set; }
        public GeneralLink PoliciesLink { get; set; }
        public GeneralLink AccessibilityLink { get; set; }
        public HtmlString Copyright { get; set; }
        public HtmlString Disclaimer { get; set; }

    }
}