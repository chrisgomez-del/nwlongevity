using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class AccordionItemViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public IHtmlString Copy { get; set; }
        public IHtmlString Image { get; set; }
        public string CardId { get; set; }
    }
}