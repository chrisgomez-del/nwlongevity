using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Global
{
    public class Footer
    {
        public List<GeneralLink> MainNavLinks { get; set; }
        public List<GeneralLink> AdditionalNavLinks { get; set; }

        public HtmlString GenericContent { get; set; }
        public string NMImage { get; set; }
        public Footer()
        {
            MainNavLinks = new List<GeneralLink>();
            AdditionalNavLinks = new List<GeneralLink>();
        }
    }
}