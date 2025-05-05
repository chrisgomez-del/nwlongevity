using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Components.Tabs
{
    public class TabLabel
    {
        public Item SourceItem { get; set; }
        public HtmlString TabName { get; set; }
    }
}