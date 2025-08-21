using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class SlideViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Copy { get; set; }
        public IHtmlString CTA { get; set; }
        public IHtmlString Image { get; set; }
        public string ImageLocation { get; set; }
        public string ImageLocationCssClass { get; set; }
    }

    public class SliderViewModel
    {
        public Item SourceItem { get; set; }
    }
}