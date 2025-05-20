using System.Web;
using NM_MultiSites.Areas.westhealth.Models.Components;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace NM_MultiSites.Areas.westhealth.Services
{
    public interface ITeamService
    {

        TeamContainerViewModel GetTeamContainer();
        TeamMemberViewModel GetTeamMember();
    }
    public class TeamService : ITeamService
    {
        public TeamContainerViewModel GetTeamContainer()
        {
            var model = new TeamContainerViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Title = new HtmlString(FieldRenderer.Render(datasource, Templates.TeamContainer.Fields.Title));
                model.Logo = new HtmlString(FieldRenderer.Render(datasource, Templates.TeamContainer.Fields.Logo));
                model.Copy = new HtmlString(FieldRenderer.Render(datasource, Templates.TeamContainer.Fields.Copy));
                model.SubTitle = new HtmlString(FieldRenderer.Render(datasource, Templates.TeamContainer.Fields.SubTitle));

            }
            return model;
        }
        public TeamMemberViewModel GetTeamMember()
        {
            var model = new TeamMemberViewModel();
            Item datasource = WestHealthSitecoreService.GetDataSourceItem();
            if (datasource != null)
            {
                model.SourceItem = datasource;
                model.Name= new HtmlString(FieldRenderer.Render(datasource, Templates.TeamMember.Fields.Name));
                model.Qualifications= new HtmlString(FieldRenderer.Render(datasource, Templates.TeamMember.Fields.Qualifications));
                model.Titles= new HtmlString(FieldRenderer.Render(datasource, Templates.TeamMember.Fields.Titles));
                model.Image= new HtmlString(FieldRenderer.Render(datasource, Templates.TeamMember.Fields.Image));
                model.ProfileLink= new HtmlString(FieldRenderer.Render(datasource, Templates.TeamMember.Fields.ProfileLink));
            }
            return model;
        }
    }
}