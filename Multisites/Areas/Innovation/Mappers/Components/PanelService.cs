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
    public interface IPanelService
    {
        BasePanel GetPanelData();
    }
    public class PanelService : IPanelService
    {
        public BasePanel GetPanelData()
        {
            BasePanel data = new BasePanel();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.Subtitle = new HtmlString(FieldRenderer.Render(datasource, "Subtitle"));
                data.Body = new HtmlString(FieldRenderer.Render(datasource, "Body"));
                data.BackgroundImage = new HtmlString(FieldRenderer.Render(datasource, "BackgroundImage"));
            }
            return data;
        }

    }
}