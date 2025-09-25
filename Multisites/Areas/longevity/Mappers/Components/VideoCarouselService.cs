using NM_MultiSites.Areas.Longevity.Models.Components;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Data.Fields;
using Sitecore.Mvc;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System.Globalization;
using System.EnterpriseServices;
using Sitecore.Web.UI.WebControls.Presentation;
using Sitecore.Shell.Framework.Commands.TemplateBuilder;

namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IVideoCarouselService
    {
        VideoCarousel GetVideoData();

    }
    public class VideoCarouselService : IVideoCarouselService
    {
        public VideoCarousel GetVideoData() 
        {
            VideoCarousel videoCarousel = new VideoCarousel();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();
            if(datasource != null)
            {
                videoCarousel.Title1 = new HtmlString(FieldRenderer.Render(datasource, "Title 1"));
                videoCarousel.Title2 = new HtmlString(FieldRenderer.Render(datasource, "Title 2"));
                videoCarousel.Class = String.IsNullOrEmpty(datasource.Fields["Class"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(datasource, "Class");

                bool isMobile = HttpContext.Current?.Request?.Browser?.IsMobileDevice ?? false;
                if (isMobile)
                {
                    // Only add the first video to the collection
                    if (datasource.Children.Count > 0)
                    {
                        Video video = new Video();
                        video.VideoPath = String.IsNullOrEmpty(datasource.Children[0].Fields["Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(datasource.Children[0], "Video");
                        videoCarousel.VideoCollection.Add(video);
                    }
                }
                else
                {
                    foreach (Sitecore.Data.Items.Item child in datasource.Children)
                    {
                        Video video = new Video();
                        video.VideoPath = String.IsNullOrEmpty(child.Fields["Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Video");
                        videoCarousel.VideoCollection.Add(video);
                    }
                }                
            }
            return videoCarousel;
        }


    }
}
