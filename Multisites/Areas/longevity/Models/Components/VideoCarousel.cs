using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Components
{
    public class VideoCarousel
    {
        public VideoCarousel()
        {
            VideoCollection = new List<Video>();
        }
        public List<Video> VideoCollection { get; set; }
        public HtmlString Title1 { get; set; }
        public HtmlString Title2 { get; set; }
        public string Class { get; set; }
    }

    public class Video
    {
        public string VideoPath { get; set; }
    }
}