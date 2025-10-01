using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Components
{
    public class FullAssessment
    {
        public FullAssessment()
        {
            AssessmentCollection = new List<Assessment>();
        }
        public List<Assessment> AssessmentCollection { get; set; }
        public HtmlString Title { get; set; }
    }

    public class Assessment
    {
        public HtmlString Title { get; set; }
        public HtmlString Content { get; set; }
        public string VideoPath { get; set; }
        public string MobileVideoPath { get; set; }
    }
}