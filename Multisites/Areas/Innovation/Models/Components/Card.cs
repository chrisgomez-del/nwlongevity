using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    public class Card : TextBlock
    {
        public string BackgroundImage { get; set; }
        public string Link { get; set; }
        public IHtmlString LinkTitle { get; set; }
    }
}