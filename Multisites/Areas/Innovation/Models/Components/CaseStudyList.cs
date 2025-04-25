using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Models.Components
{
    public class CaseStudyList
    {
        public CaseStudyList()
        {
            CaseStudies = new List<CaseStudy>();
        }
        public HtmlString Title { get; set; }
       
        public List<CaseStudy> CaseStudies { get; set; }
    }
}