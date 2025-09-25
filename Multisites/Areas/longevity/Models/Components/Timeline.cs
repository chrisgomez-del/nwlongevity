using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Components
{
    public class Timeline
    {
        public Timeline()
        {
            EventCollection = new List<TimelineEvent>();
        }
        public HtmlString Title { get; set; }
        public List<TimelineEvent> EventCollection { get; set; }
    }

    public class TimelineEvent
    {
        public HtmlString Title { get; set; }
        public HtmlString Content { get; set; }
        public HtmlString NavTitle { get; set; }
        public string VideoPath { get; set; }
        public string VideoAriaLabel { get; set; }
        public string ImagePath { get; set; }
        public string ImageAlt { get; set; }
        public string InfoBoxTop { get; set; }
        public string InfoBoxLeft { get; set; }
    }
}