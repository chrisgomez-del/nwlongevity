using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.westhealth.Models.Navigation
{
    public class FooterViewModel
    {
        public List<NavigationLinkViewModel> Links { get; set; } = new List<NavigationLinkViewModel>();
        public List<SocialLinkViewModel> SocialLinks{ get; set; } = new List<SocialLinkViewModel>();
        public string Logo { get; set; }
        public IHtmlString Copy { get; set; }
        public List<GenericLinkViewModel> FooterUtilityLinks { get; set; } = new List<GenericLinkViewModel>();
        public FooterViewModel() { }
    }
}