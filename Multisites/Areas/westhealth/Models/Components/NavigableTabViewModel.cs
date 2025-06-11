using System.Collections.Generic;
using System.Web;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class NavigableTabViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public string TabId { get; set; }
        public List<TabResourceViewModel> TabResources { get; set; }
    }
}