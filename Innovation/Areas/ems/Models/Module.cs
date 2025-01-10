using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMS.ems.Models
{
    public class Module
    {
        public Module() { }
        public HtmlString Title { get; set; }
        public HtmlString ShortText { get; set; }
        public HtmlString MainContent { get; set; }
        public HtmlString link { get; set; }
        public HtmlString CTATitle { get; set; }
        public String CTALink { get; set; }
        public String Image { get; set; }
        public String ImageFullPath { get; set; }
        public String ItemId { get; set; }
        public Item Item { get; set; }
        public Boolean ImagePosition { get; set; }
    }
}