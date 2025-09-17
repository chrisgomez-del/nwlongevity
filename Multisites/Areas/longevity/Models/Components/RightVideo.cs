using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Components
{
    public class RightVideo
    {
        public HtmlString Title { get; set; }
        public HtmlString Content { get; set; }
        public string CTALink { get; set; }
        public string CTATitle { get; set; }
        public string BackgroundVideoPath { get; set; }
    }
}