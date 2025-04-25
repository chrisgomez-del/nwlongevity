using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class BasePanel
    {
        public Item SourceItem { get; set; }
        public HtmlString Title { get; set; }
        public HtmlString Subtitle { get; set; }
        public HtmlString Body { get; set; }
        public HtmlString BackgroundImage { get; set; }
    }
}