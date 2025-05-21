using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class CardViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString SubTitle { get; set; }
        public IHtmlString Copy { get; set; }            
        public IHtmlString CtaText { get; set; }
        public string CtaSource { get; set; }

    }
}