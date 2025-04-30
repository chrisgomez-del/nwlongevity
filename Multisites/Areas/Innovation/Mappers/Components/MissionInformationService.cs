using NM_MultiSites.Areas.Innovation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using NM_MultiSites.Areas.Innovation.Models;
using NM_MultiSites.Areas.Innovation.Models.Components;
using NM_MultiSites.Areas.Innovation.API.Youtube.Models;
using NM_MultiSites.Areas.Innovation.Infrastructure.Extensions;
using NM_MultiSites.Areas.Innovation.API.Youtube;

namespace NM_MultiSites.Areas.Innovation.Mappers.Components
{
    public interface IMissionInformationService
    {
        MissionInformation GetMissionInfoData();
        YoutubeVideo getYTVideo(string VideoUrl);
    }
    public class MissionInformationService : IMissionInformationService
    {
        public MissionInformation GetMissionInfoData()
        {
            MissionInformation missionInformation = new MissionInformation();
            Item datasource = SitecoreAccess.GetDataSourceItem();
            if (datasource != null)
            {
                missionInformation.SourceItem = datasource;
                missionInformation.MissionInfo = new HtmlString(FieldRenderer.Render(datasource, "Information"));
                missionInformation.MissionImage = new HtmlString(FieldRenderer.Render(datasource, "Image"));
                missionInformation.Title = new HtmlString(FieldRenderer.Render(datasource, "Title"));
                missionInformation.Video = FieldRenderer.Render(datasource, "Video");
                missionInformation.IsVideoAvailable = !String.IsNullOrWhiteSpace(missionInformation.Video);
                missionInformation.ytVideo = missionInformation.IsVideoAvailable ? getYTVideo(missionInformation.Video) : null;
            }
            return missionInformation;
        }
        public YoutubeVideo getYTVideo(string VideoUrl)
        {
            string videoID = VideoUrl.ParseYouTubeId();
            YoutubeVideo ytVideo = YoutubeService.CreateDefault().GetVideoForSingleId(videoID);
            return ytVideo;

        }
    }
}