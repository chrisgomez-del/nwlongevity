using Innovation.Areas.Innovation.API.Youtube;
using Innovation.Areas.Innovation.API.Youtube.Models;
using Innovation.Areas.Innovation.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innovation.Areas.Innovation.Helpers
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