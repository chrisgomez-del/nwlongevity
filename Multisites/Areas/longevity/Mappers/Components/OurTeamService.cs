using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;


namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IOurTeamService
    {
        OurTeam GetOurTeamData();

    }
    public class OurTeamService : IOurTeamService
    {
        public OurTeam GetOurTeamData()
        {
            OurTeam OurTeam = new OurTeam();

            return OurTeam;
        }

    }
}
