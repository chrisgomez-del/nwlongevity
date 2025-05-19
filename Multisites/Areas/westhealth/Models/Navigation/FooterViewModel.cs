using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.westhealth.Models.Navigation
{
    public class FooterViewModel
    {
        public List<GenericLinkViewModel> Links { get; set; } = new List<GenericLinkViewModel>();
        public List<GenericLinkViewModel> SocialLinks{ get; set; } = new List<GenericLinkViewModel>();
        public string Logo { get; set; }
        public FooterViewModel() { }
    }
}