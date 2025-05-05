using System;
using Google.Apis.YouTube.v3.Data;


namespace NM_MultiSites.Areas.Innovation.API.Youtube.Models
{
    public class YoutubeVideo
    {
        private Video source;

        private TimeSpan vDuration = TimeSpan.MinValue;

        public YoutubeVideo(Video video)
        {
            this.source = video;
        }

        public string Id
        {
            get
            {
                return this.source.Id;
            }
        }

        public string Title
        {
            get
            {
                return this.source.Snippet.Title;
            }
        }

        public string Description
        {
            get
            {
                return this.source.Snippet.Description;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                if (vDuration.Equals(TimeSpan.MinValue))
                {
                    string vDuration = this.source.ContentDetails?.Duration;

                    if (!string.IsNullOrWhiteSpace(vDuration))
                    {
                        /* Subtract 1 second due to Youtube displaying duration incorrectly when video first opens.
                         * https://github.com/sampotts/plyr/issues/362
                         * https://code.google.com/p/gdata-issues/issues/detail?id=8690
                         * https://laughlindev.visualstudio.com/NORMED/_workitems/edit/9757
                         */
                        this.vDuration = System.Xml.XmlConvert.ToTimeSpan(vDuration).Subtract(new TimeSpan(0, 0, 1));
                    }
                }

                return this.vDuration;
            }
        }

        public string ThumbnailUrl
        {
            get
            {
                var thumb = "";

                if (this.source.Snippet.Thumbnails != null)
                {
                    if (this.source.Snippet.Thumbnails.Maxres != null)
                    {
                        thumb = this.source.Snippet.Thumbnails.Maxres.Url;
                    }
                    else if (this.source.Snippet.Thumbnails.High != null)
                    {
                        thumb = this.source.Snippet.Thumbnails.High.Url;
                    }
                    else if (this.source.Snippet.Thumbnails.Standard != null)
                    {
                        thumb = this.source.Snippet.Thumbnails.Standard.Url;
                    }
                    else if (this.source.Snippet.Thumbnails.Medium != null)
                    {
                        thumb = this.source.Snippet.Thumbnails.Medium.Url;
                    }
                    else
                    {
                        thumb = this.source.Snippet.Thumbnails.Default__.Url;
                    }
                }

                return thumb;
            }
        }

        public DateTime? PublishDate
        {
            get
            {
                DateTime? newDate;

                try
                {
                    newDate = this.source.Snippet.PublishedAt;
                }
                catch (Exception)
                {
                    return null;
                }

                return newDate;
            }
        }
    }
}