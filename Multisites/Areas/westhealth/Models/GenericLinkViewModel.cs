using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.westhealth.Models
{
    public class GenericLinkViewModel
    {
        public string LinkSource { get; set; }
        public IHtmlString LinkText { get; set; }
        public string Icon { get; set; }
    }
    public class NavigationLinkViewModel: GenericLinkViewModel
    {
        public List<NavigationLinkViewModel> Children { get; set; } = new List<NavigationLinkViewModel>();
    }
}