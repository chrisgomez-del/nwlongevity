using NM_MultiSites.Areas.Innovation.API.Youtube;
using NM_MultiSites.Areas.Innovation.API.Youtube.Models;
using NM_MultiSites.Areas.Innovation.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.Helpers
{
    public class YTVideo
    {
        public YoutubeVideo getYTVideo(string VideoUrl)
        {
            string videoID = VideoUrl.ParseYouTubeId();
            YoutubeVideo ytVideo = YoutubeService.CreateDefault().GetVideoForId(videoID);
            return ytVideo;
            
        }
    }
}