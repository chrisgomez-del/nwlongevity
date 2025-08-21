using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Pages
{
    public class ArticleViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Subtitle { get; set; }
        public IHtmlString Author { get; set; }
        public IHtmlString Source { get; set; }
        public IHtmlString Date { get; set; }
        public IHtmlString Image { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public IHtmlString Copy { get; set; }
    }
}