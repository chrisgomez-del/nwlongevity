using System;
using System.Web;
using NM_MultiSites.Areas.Longevity.Models.Components;
using NM_MultiSites.Areas.Longevity.Helpers;
using Sitecore.Web.UI.WebControls;
using Sitecore.Collections;

namespace NM_MultiSites.Areas.Longevity.Mappers.Components
{
    public interface ITimelineService
    {
        Timeline GetTimelineData();

    }
    public class TimelineService : ITimelineService
    {
        public Timeline GetTimelineData()
        {
            Timeline timeline = new Timeline();
            Sitecore.Data.Items.Item datasource = SitecoreAccess.GetDataSourceItem();

            if (datasource != null)
            {
                timeline.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));

                ChildList children = datasource.GetChildren();
                foreach (Sitecore.Data.Items.Item child in children)
                {
                    TimelineEvent timelineEvent = new TimelineEvent();

                    timelineEvent.Title = new HtmlString(FieldRenderer.Render(child, "Title"));
                    timelineEvent.Content = new HtmlString(FieldRenderer.Render(child, "Content"));
                    timelineEvent.NavTitle = new HtmlString(FieldRenderer.Render(child, "Nav Title"));
                    timelineEvent.VideoPath = String.IsNullOrEmpty(child.Fields["Video"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Video");
                    timelineEvent.VideoAriaLabel = String.IsNullOrEmpty(child.Fields["Video Aria Label"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Video Aria Label");
                    timelineEvent.ImagePath = String.IsNullOrEmpty(child.Fields["Image"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Image");
                    timelineEvent.ImageAlt = String.IsNullOrEmpty(child.Fields["Image Alt"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Image Alt");
                    timelineEvent.InfoBoxTop = String.IsNullOrEmpty(child.Fields["Info Box Top"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Info Box Top");
                    timelineEvent.InfoBoxLeft = String.IsNullOrEmpty(child.Fields["Info Box Left"].GetValue(true)) ? null : SitecoreAccess.GetFieldValue(child, "Info Box Left");
                    timelineEvent.MobileImagePath = String.IsNullOrEmpty(child.Fields["Mobile Image"].GetValue(true)) ? null : SitecoreAccess.GetMediaUrl(child, "Mobile Image");
                    timeline.EventCollection.Add(timelineEvent);
                    
                }
            }

            return timeline;
        }

    }
}
