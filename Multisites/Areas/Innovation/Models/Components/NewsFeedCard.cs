using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class NewsFeedCard
    {
        public Item SourceItem { get; set; }

        public virtual HtmlString Heading { get; set; }

        public virtual HtmlString SubHeading { get; set; }

        public virtual string CTALink { get; set; }

        public virtual HtmlString CTAText { get; set; }
    }
}