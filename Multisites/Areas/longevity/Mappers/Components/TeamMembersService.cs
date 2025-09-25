using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using NM_MultiSites.Areas.Innovation.Models.Components;
using NM_MultiSites.Areas.Longevity.Models;

namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface ITeamMembersService
    {
        TeamMembers GetTeamMembersData();

    }
    public class TeamMembersService : ITeamMembersService
    {
        public TeamMembers GetTeamMembersData()
        {
            TeamMembers teamMembers = new TeamMembers();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();

            if (datasource != null)
            {
                foreach (Sitecore.Data.Items.Item child in datasource.Children)
                {
                    TeamMemberRow row = new TeamMemberRow();
                    row.Class = String.IsNullOrEmpty(child.Fields["Class"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Class");
                    
                    foreach (Sitecore.Data.Items.Item child2 in child.Children) {

                        TeamMember teamMember = new TeamMember();
                        teamMember.ProfileImagePath = String.IsNullOrEmpty(child2.Fields["Profile Image"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child2, "Profile Image");
                        teamMember.Title = new HtmlString(FieldRenderer.Render(child2, "Title"));
                        GeneralLink name = new GeneralLink()
                        {
                            Title = new HtmlString(SitecoreAccess.LinkTitle(child2.Fields["Name"])),
                            CTALink = String.IsNullOrEmpty(child2.Fields["Name"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(child2.Fields["Name"]),
                        };
                        teamMember.Name = name;
                        row.TeamMemberCollection.Add(teamMember);
                    }
                    teamMembers.TeamMemberRowCollection.Add(row);
                }
            }

            return teamMembers;
        }

    }
}
