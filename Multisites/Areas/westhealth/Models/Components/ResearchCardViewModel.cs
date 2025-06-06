using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class ResearchCardViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Source { get; set; }
        public string CtaSource { get; set; }
        public string CtaText { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundCssClass { get; set; }
    }
}