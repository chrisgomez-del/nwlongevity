using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models
{
    public class GeneralLink
    {
        public HtmlString Title { get; set; }
        public string CTALink { get; set; }
        public Item Item { get; set; }
    }
}