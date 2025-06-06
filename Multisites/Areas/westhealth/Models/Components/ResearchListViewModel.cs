using System.Collections.Generic;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.westhealth.Models.Components
{
    public class ResearchListViewModel
    {
        public Item SourceItem { get; set; }
        public IHtmlString Title { get; set; }
        public ReferenceField CardLocation { get; set; }
        public List<ResearchCardViewModel> ResearchCards { get; set; } = new List<ResearchCardViewModel>();
    }
}