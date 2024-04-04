using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class EventList
    {
        public EventList()
        {
            Events = new List<Event>();
        }
        public HtmlString Title { get; set; }
        public HtmlString Description { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundClass { get; set; }
        public List<Event> Events { get; set; }
        
    }
}