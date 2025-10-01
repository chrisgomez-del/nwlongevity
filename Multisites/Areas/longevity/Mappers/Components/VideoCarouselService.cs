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
using Sitecore.Collections;

namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface IVideoCarouselService
    {
        VideoCarousel GetVideoData();

    }
    public class VideoCarouselService : IVideoCarouselService
    {
        public const string VIDEOMOBILETEMPLATEID = "{D587E3A0-40CF-4C85-ABD6-9B5EEFAC87A7}";

        public VideoCarousel GetVideoData() 
        {
            VideoCarousel videoCarousel = new VideoCarousel();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();
            if(datasource != null)
            {
                videoCarousel.Title1 = new HtmlString(FieldRenderer.Render(datasource, "Title 1"));
                videoCarousel.Title2 = new HtmlString(FieldRenderer.Render(datasource, "Title 2"));
                videoCarousel.Description = new HtmlString(FieldRenderer.Render(datasource, "Description"));
                videoCarousel.Class = String.IsNullOrEmpty(datasource.Fields["Class"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(datasource, "Class");

                bool isMobile = HttpContext.Current?.Request?.Browser?.IsMobileDevice ?? false;
                //if (isMobile)
                //{
                    // Only add the first video to the collection
                //    if (datasource.Children.Count > 0)
                //    {
                //        Video video = new Video();
                //        video.VideoPath = String.IsNullOrEmpty(datasource.Children[0].Fields["Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(datasource.Children[0], "Video");
                //        videoCarousel.VideoCollection.Add(video);
                //    }
                //}
                //else
                //{
                ChildList children = datasource.GetChildren();
                foreach (Sitecore.Data.Items.Item child in children)
                {
                    Video video = new Video();
                        
                    if (child.TemplateID.ToString() == VIDEOMOBILETEMPLATEID) {
                        video.IsMobile = true;
                    }
                        
                    video.VideoPath = String.IsNullOrEmpty(child.Fields["Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Video");
                    videoCarousel.VideoCollection.Add(video);
                }
                //}                
            }
            return videoCarousel;
        }


    }
}
