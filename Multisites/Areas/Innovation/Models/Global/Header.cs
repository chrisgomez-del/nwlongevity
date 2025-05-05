using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Global
{
    public class Header
    {
        public Item SourceItem { get; set; }
        public HtmlString Title { get; set; }

        public List<GeneralLink> MainNavLinks { get; set; }
        public List<GeneralLink> AdditionalNavLinks { get; set; }

        public HtmlString GenericContent { get; set; }
        public HtmlString SubTitle { get; set; }
        public bool IsHomePageHeader { get; set; }

        public HtmlString CaseStudyImage { get; set; }

        public HtmlString AdditionalContent { get; set; }

        public Header()
        {
            MainNavLinks = new List<GeneralLink>();
            AdditionalNavLinks = new List<GeneralLink>();
        }
        
    }
}