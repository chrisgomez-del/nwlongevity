using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class EnhancedCalloutViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Copy { get; set; }
    }
}