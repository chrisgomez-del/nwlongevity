using Innovation.Areas.Innovation.Helpers;
using Innovation.Areas.Innovation.Models.Components;
using Innovation.Areas.Innovation.Models.Components.Tabs;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Mappers.Components
{
    public interface IFocusAreaService
    {
        FocusAreaCard GetFocusAreaData();
        TabLabel GetTabLabel();
        ContentTab GetContentTab();
        TabPanel GetTabPanel();
    }
    public class FocusAreaService : IFocusAreaService
    {

        public FocusAreaCard GetFocusAreaData()
        {
            FocusAreaCard data = new FocusAreaCard();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Image = new HtmlString(FieldRenderer.Render(datasource, "Image"));
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.Body = new HtmlString(FieldRenderer.Render(datasource, "Body"));
            }
            return data;
        }

        public TabLabel GetTabLabel()
        {
            TabLabel data = new TabLabel();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null) 
            {
                data.SourceItem = datasource;
                data.TabName = new HtmlString(FeedRenderer.RenderField(datasource, "TabName"));
            }
            return data;
        }

        public ContentTab GetContentTab()
        {
            ContentTab data = new ContentTab();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Image = new HtmlString(FieldRenderer.Render(datasource, "Image"));
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.Body = new HtmlString(FieldRenderer.Render(datasource, "Body"));
            }
            return data;
        } 

        public TabPanel GetTabPanel()
        {
            TabPanel data = new TabPanel();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                var lf = datasource.Fields["Link"];
                data.SourceItem = datasource;
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.Link = String.IsNullOrEmpty(lf.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf);
                data.LinkTitle = new HtmlString(FieldRenderer.Render(datasource, "LinkTitle"));
    }
            return data;
        }
    }
}