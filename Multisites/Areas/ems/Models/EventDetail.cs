using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.Areas.ems.Models
{
    public class EventDetail
    {
        public EventDetail() { }
        public HtmlString Title { get; set; }
        public HtmlString ShortText { get; set; }
        public HtmlString MainContent { get; set; }
        public HtmlString AdditionalContent { get; set; }
        public HtmlString CTATitle { get; set; }
        public HtmlString StartDate { get; set; }
        public HtmlString EndDate { get; set; }
        public String Image { get; set; }
        public String ImageFullPath { get; set; }
        public String CTALink { get; set; }
        public Item Item { get; set; }
    }
}