using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class AccordionItemViewModel
    {
        public Item SourceItem { get; set; }
        public HtmlString Title { get; set; }
        public HtmlString Copy { get; set; }
        public HtmlString Image { get; set; }
    }
}