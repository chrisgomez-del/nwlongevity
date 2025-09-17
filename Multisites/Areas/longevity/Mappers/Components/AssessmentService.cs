using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;


namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IAssessmentService
    {
        Assessment GetAssessmentData();

    }
    public class AssessmentService : IAssessmentService
    {
        public Assessment GetAssessmentData() 
        {
            Assessment assessment = new Assessment();
            
            return assessment;
        }

    }
}
