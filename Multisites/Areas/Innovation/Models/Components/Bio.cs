using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    public class Bio
    {
        public Item SourceItem { get; set; }
        public IHtmlString Image { get; set; }
        public IHtmlString Name { get; set; }
        public IHtmlString LongJobTitle { get; set; }
        public IHtmlString Profile_LeftColumn { get; set; }
        public IHtmlString Profile_RightColumn { get; set; }
    }
}