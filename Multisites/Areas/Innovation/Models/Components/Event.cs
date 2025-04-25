using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public interface IEvent
    {
         HtmlString Type { get; set; }
         HtmlString Title { get; set; }
         string Link { get; set; }
         string LinkTitle { get; set; }
         string DateFrom { get; set; }
         string DateTo { get; set; }
         HtmlString DateString { get; set; }
         HtmlString Location { get; set; }
    }
    public class Event : IEvent
    {
        public HtmlString Type { get; set; }
        public HtmlString Title { get; set; }
        public string Link { get; set; }
        public string LinkTitle { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public HtmlString DateString { get; set; }
        public HtmlString Location { get; set; }
    }
}