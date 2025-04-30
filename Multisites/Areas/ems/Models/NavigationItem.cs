using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Models
{
    public class NavigationItem
    {
        public NavigationItem() { }
        public String Title { get; set; }
        public String URL { get; set; }
        public bool Active { get; set; }
        public String itemid { get; set; }
        public bool IsSecurePage { get; set; }
        public IEnumerable<NavigationItem> Children { get; set; }
    }
}