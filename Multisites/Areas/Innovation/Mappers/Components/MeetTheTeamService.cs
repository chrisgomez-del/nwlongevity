using NM_MultiSites.Areas.Innovation.Helpers;
using NM_MultiSites.Areas.Innovation.Models.Components;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Mappers.Components
{
    public interface IMeetTheTeamService
    {
        Bio GetBioData();
    }
    public class MeetTheTeamService : IMeetTheTeamService
    {
       
        public Bio GetBioData()
        {
            Bio data = new Bio();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Image = new HtmlString(FieldRenderer.Render(datasource, "Image"));
                data.Name = new HtmlString(FieldRenderer.Render(datasource, "Name"));
                data.LongJobTitle = new HtmlString(FieldRenderer.Render(datasource, "LongJobTitle"));
                data.Profile_LeftColumn = new HtmlString(FieldRenderer.Render(datasource, "Profile_LeftColumn"));
                data.Profile_RightColumn = new HtmlString(FieldRenderer.Render(datasource, "Profile_RightColumn"));

            }
            return data;
        }
    }
}