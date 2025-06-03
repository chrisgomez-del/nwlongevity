using System.Collections.Generic;
using System.Web;

namespace NM_MultiSites.Areas.westhealth.Models.Navigation
{
    public class InternalNavigationViewModel 
    {
        public List<InternalNavigationLinkViewModel> Links { get; set; } = new List<InternalNavigationLinkViewModel>(); 

    }
    public class InternalNavigationLinkViewModel
    {
        public IHtmlString Title { get; set; }
        public IHtmlString SectionId { get; set; }
    }
}