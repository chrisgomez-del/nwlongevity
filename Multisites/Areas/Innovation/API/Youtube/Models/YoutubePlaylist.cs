using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NM_MultiSites.Areas.Innovation.API.Youtube.Models
{
    public class YoutubePlaylist
    {
        public enum DisplayModes
        {
            DEFAULT,
            LINK_ONLY
        }

        private Playlist source;

        public YoutubePlaylist(Playlist source)
        {
            this.source = source;
        }

        public string Title
        {
            get
            {
                return this.source.Snippet.Title;
            }
        }

        public string ThumbnailUrl
        {
            get
            {
                return this.source.Snippet.Thumbnails.Default__.Url;
            }
        }

        public string Id
        {
            get
            {
                return this.source.Id;
            }
        }

        public string Description
        {
            get
            {
                return this.source.Snippet.Description;
            }
        }
    }
}