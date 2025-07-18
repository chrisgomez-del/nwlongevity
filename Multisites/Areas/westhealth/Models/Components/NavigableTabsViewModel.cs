using System.Collections.Generic;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class NavigableTabsViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public ReferenceField NavigableTabsLocation { get; set; }
        public List<NavigableTabViewModel> NavigableTabs { get; set; }
        public string SectionId { get; set; }
    }
}