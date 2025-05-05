using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    public class CaseStudyPagination
    {
        public bool ShowonPage { get; set; }
        public string PreviousUrl { get; set; }
        public string PortfolioUrl { get; set; }
        public string NextUrl { get; set; }

    }
}