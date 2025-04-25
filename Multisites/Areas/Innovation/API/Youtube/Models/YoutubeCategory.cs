using Google.Apis.YouTube.v3.Data;
using Innovation.Areas.Innovation.API.Youtube.Interfaces;

namespace Innovation.Areas.Innovation.API.Youtube.Models
{
    public class YoutubeCategory
    {
        private VideoCategory source;
        private IYoutubeService vidStore;

        public YoutubeCategory(VideoCategory source, IYoutubeService vidStore)
        {
            this.source = source;
            this.vidStore = vidStore;
        }

        public string Name
        {
            get
            {
                return this.source.Snippet.Title;
            }
        }

        /*
        public List<YoutubeVideo> Videos
        {
            get
            {
                var vids = this.vidStore.GetVideosForCategory(this);

                return vids;
            }
        }
        */

        public string Id
        {
            get
            {
                return this.source.Id;
            }
        }
    }
}