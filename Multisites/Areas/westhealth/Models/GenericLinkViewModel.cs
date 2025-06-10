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
    }
    public class SocialLinkViewModel : GenericLinkViewModel
    {
        public string LinkIconClass { get; set; }
    }
    public class NavigationLinkViewModel : GenericLinkViewModel
    {
        public bool IsActive { get; set; }
        public List<NavigationLinkViewModel> Children { get; set; } = new List<NavigationLinkViewModel>();
    }
}