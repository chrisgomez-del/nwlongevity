using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    public class StatCardsImage
    {
        public Item SourceItem { get; set; }

        public virtual HtmlString Image { get; set; }

        public virtual HtmlString Title { get; set; }

        public virtual HtmlString Body { get; set; }

        public virtual string Action { get; set; }

        public virtual HtmlString CTAText { get; set; }
    }
}