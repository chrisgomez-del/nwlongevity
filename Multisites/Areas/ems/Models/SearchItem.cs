using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Models
{
    public class SearchItem
    {
        public SearchItem() { }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }
        public String Type { get; set; }
    }
}