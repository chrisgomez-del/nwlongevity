using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NM_MultiSites.Areas.Innovation.API.Youtube.Models;
using NM_MultiSites.Areas.Innovation.Helpers;
using Sitecore.Data.Items;

namespace NM_MultiSites.Areas.Innovation.Models.Components
{
    public class MissionInformation
    {
        public Item SourceItem { get; set; }
        public HtmlString MissionInfo { get; set; }
        public HtmlString Title { get; set; }
        public string Video { get; set; }
        public bool IsVideoAvailable { get; set; }
        public YoutubeVideo ytVideo { get; set; }
        public IHtmlString MissionImage { get; set; }
        public IHtmlString VideoStillImage { get; set; }
        public bool MissionImageOnLeft { get; set; }
    } 
}