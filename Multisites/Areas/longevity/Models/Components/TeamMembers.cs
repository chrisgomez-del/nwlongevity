using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Longevity.Models.Components
{
    public class TeamMembers
    {
        public TeamMembers()
        {
            TeamMemberRowCollection = new List<TeamMemberRow>();
        }
        public List<TeamMemberRow> TeamMemberRowCollection { get; set; }
    }

    public class TeamMemberRow {

        public TeamMemberRow()
        {
            TeamMemberCollection = new List<TeamMember>();
        }
        public List<TeamMember> TeamMemberCollection { get; set; }
        public string Class { get; set; }
    }
    
    public class TeamMember
    {
        public GeneralLink Name { get; set; }
        public HtmlString Title { get; set; }
        public string ProfileImagePath { get; set; }
    }
}