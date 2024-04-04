using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class StatCardsDataPoint
    {
        public Item SourceItem { get; set; }

        public virtual HtmlString SubTitle { get; set; }

        public virtual HtmlString Number { get; set; }

        public virtual HtmlString Body { get; set; }

        public virtual string Action { get; set; }

        public virtual HtmlString CTAText { get; set; }
    }
}