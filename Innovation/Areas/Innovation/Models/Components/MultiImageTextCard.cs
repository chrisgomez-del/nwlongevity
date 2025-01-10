using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class MultiImageTextCard
    {
        public Item SourceItem { get; set; }
        public IHtmlString VerticalSection_Title { get; set; }
        public string VerticalSection_BackgroundImage { get; set; }
        public IHtmlString VerticalSection_Body { get; set; }
        public string VerticalSection_Link { get; set; }
        public IHtmlString VerticalSection_LinkTitle { get; set; }
        public IHtmlString HorizontalSection_Title { get; set; }
        public string HorizontalSection_BackgroundImage { get; set; }
        public IHtmlString HorizontalSection_Body { get; set; }
        public string HorizontalSection_Link { get; set; }
        public IHtmlString HorizontalSection_LinkTitle { get; set; }
        public IHtmlString FirstImage { get; set; }
        public IHtmlString SecondImage { get; set; }

    }
}