using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    public interface ITextBlock
    {
        Item SourceItem { get; set; }
        IHtmlString Title { get; set; }
        IHtmlString Body { get; set; }
    }
    public class TextBlock : ITextBlock
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Body { get; set; }
    }
}