using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    
    public class CaseStudy 
    {
        public Item SourceItem { get; set; }
        public string BackgroundImage { get; set; }
        public HtmlString Title { get; set; }
        public string Link { get; set; }
        public HtmlString CTATitle { get; set; }
        
    }
}