using Innovation.Areas.Innovation.Helpers;
using Innovation.Areas.Innovation.Models.Components;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Mappers.Components
{
    public interface IStatsComponentService
    {
        StatCardsDataPoint GetStatCardsDataPoint();

        StatCardsImage GetStatCardsImage();

        StatComponentPanel GetStatsComponentPanel();
    }

    public class StatsComponentService : IStatsComponentService
    {

        public StatCardsDataPoint GetStatCardsDataPoint()
        {
            StatCardsDataPoint data = new StatCardsDataPoint();
            Item datasource = SitecoreAccess.GetDataSourceItem();

            if (datasource != null)
            {
                var lf = datasource.Fields["Action"];
                data.SourceItem = datasource;
                data.SubTitle = new HtmlString(FieldRenderer.Render(datasource, "SubTitle"));
                data.Number = new HtmlString(FieldRenderer.Render(datasource, "Number"));
                data.Body = new HtmlString(FieldRenderer.Render(datasource, "Body"));
                data.Action = String.IsNullOrEmpty(lf.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf);
                data.CTAText = new HtmlString(FieldRenderer.Render(datasource, "CTAText"));
                
            }
            return data;

        }

        public StatCardsImage GetStatCardsImage()
        {
            StatCardsImage data = new StatCardsImage();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                var lf = datasource.Fields["Action"];
                data.SourceItem = datasource;
                data.Image = new HtmlString(FieldRenderer.Render(datasource, "Image"));
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                data.Body = new HtmlString(FieldRenderer.Render(datasource, "Body"));
                data.Action = String.IsNullOrEmpty(lf.GetValue(true)) ? null : SitecoreAccess.LinkUrl(lf);
                data.CTAText = new HtmlString(FieldRenderer.Render(datasource, "CTAText"));


            }
            return data;

        }

        public StatComponentPanel GetStatsComponentPanel()
        {
            StatComponentPanel data = new StatComponentPanel();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                data.SourceItem = datasource;
                data.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
            }
            return data;

        }


    }
}