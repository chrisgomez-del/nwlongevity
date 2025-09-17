using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using Sitecore.Shell.Framework.Commands.TemplateBuilder;


namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IRightVideoService
    {
        RightVideo GetRightVideoData();

    }
    public class RightVideoService : IRightVideoService
    {
        public RightVideo GetRightVideoData()
        {
            RightVideo rightVideo = new RightVideo();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                rightVideo.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                rightVideo.Content = new HtmlString(FieldRenderer.Render(datasource, "Content"));
                rightVideo.CTALink = ""; // String.IsNullOrEmpty(datasource.Fields["CTA"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(datasource.Fields["CTA"]);
                rightVideo.CTATitle = "What to Expect"; //String.IsNullOrEmpty(datasource.Fields["CTA"].GetValue(true)) ? null : SitecoreAccess.LinkTitle(datasource.Fields["CTA"]);
                rightVideo.BackgroundVideoPath = String.IsNullOrEmpty(datasource.Fields["Background Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(datasource, "Background Video");
            }
            return rightVideo;
        }

    }
}
