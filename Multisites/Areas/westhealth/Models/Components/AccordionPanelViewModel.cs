using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class AccordionPanelViewModel
    {
        public Item SourceItem { get; set; }
        public HtmlString Title { get; set; }
    }
}