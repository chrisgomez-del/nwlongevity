using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Models
{
    public class EventCollection
    {
        public EventCollection()
        {
            Events = new List<EventDetail>();
        }

        public HtmlString Title { get; set; }
        public List<EventDetail> Events { get; set; }
    }
}