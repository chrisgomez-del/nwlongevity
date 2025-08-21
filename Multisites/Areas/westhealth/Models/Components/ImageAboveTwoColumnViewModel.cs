using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class ImageAboveTwoColumnViewModel
    {

        public Item SourceItem { get; set; }
        public string SectionId { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Copy { get; set; }
        public string CtaSource { get; set; }
        public string CtaText { get; set; }
        public IHtmlString CardListTitle { get; set; }
        public IHtmlString Image { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
    }
}