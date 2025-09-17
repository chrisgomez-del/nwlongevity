using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;


namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IExperienceService
    {
        Experience GetExperienceData();

    }
    public class ExperienceService : IExperienceService
    {
        public Experience GetExperienceData()
        {
            Experience Experience = new Experience();

            return Experience;
        }

    }
}
