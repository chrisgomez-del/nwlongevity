using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.westhealth.Models.Navigation
{
    public class HeaderViewModel
    {
        public List<NavigationLinkViewModel> Links { get; set; } = new List<NavigationLinkViewModel>();
        public string Logo { get;set; }
        public HeaderViewModel() { 
        
        }
    }
}