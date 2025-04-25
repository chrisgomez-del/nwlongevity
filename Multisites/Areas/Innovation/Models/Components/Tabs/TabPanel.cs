using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Innovation.Areas.Innovation.Models.Components.Tabs
{
    public class TabPanel
    {
        public Item SourceItem { get; set; }
        public HtmlString Title { get; set; }
        public string Link { get; set; }
        public HtmlString LinkTitle { get; set; }
    }
}