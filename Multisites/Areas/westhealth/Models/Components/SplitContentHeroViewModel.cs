using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class SplitContentHeroViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Image { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundCssClass { get; set; }
        public string ImageLocation { get; set; }
        public string ImageLocationCssClass { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }     
    }
}