using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class StatComponentPanel
    {
        public Item SourceItem { get; set; }
        public virtual HtmlString Title { get; set; }
    }
}