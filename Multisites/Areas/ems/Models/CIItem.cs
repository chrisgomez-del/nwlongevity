using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.ems.Models
{
    public class CIItem
    {
        public CIItem() { }
        public String Title { get; set; }
        public String Npi { get; set; }
        public String Url { get; set; }
        public String Date { get; set; }
    }
}