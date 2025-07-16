using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class AccordionPanelViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Image { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
    }
}