using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.API.Youtube.Models
{
    public class Secrets
    {
        
        public HtmlString YoutubeApiKey { get; set; }
       
        public HtmlString YoutubeChannelId { get; set; }
        
        public HtmlString GooglePrivateKey { get; set; }
    }
}