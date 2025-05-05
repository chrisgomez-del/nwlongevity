using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NM_MultiSites.Areas.Innovation.Models.Components.Tabs
{
    public class ContentTab
    {
        public Item SourceItem { get; set; }
        public HtmlString Image { get; set; }
        public HtmlString Title { get; set; }
        public HtmlString Body { get; set; }
    }
}