using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using NM_MultiSites.Areas.Longevity.Models;
using Sitecore.Mvc.Helpers;

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

                GeneralLink cta = new GeneralLink()
                {
                    Title = new HtmlString(SitecoreAccess.LinkTitle(datasource.Fields["CTA"])),
                    CTALink = String.IsNullOrEmpty(datasource.Fields["CTA"].GetValue(true)) ? null : SitecoreAccess.LinkUrl(datasource.Fields["CTA"]),
                };

                rightVideo.CTA = cta;
                
                rightVideo.BackgroundVideoPath = String.IsNullOrEmpty(datasource.Fields["Background Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(datasource, "Background Video");
            }
            return rightVideo;
        }

    }
}
