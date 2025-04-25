using Innovation.Areas.Innovation.API.Youtube.Models;
using System.Collections.Generic;

namespace Innovation.Areas.Innovation.API.Youtube.Interfaces
{
    public interface IYoutubeService
    {
        List<YoutubePlaylist> GetAllPlayLists();
        YoutubeCategory GetCategory(string categoryId);
        YoutubeVideo GetVideoForId(string videoId);
        List<YoutubeVideo> GetVideosForIds(List<string> videoIds);
        List<YoutubeVideo> GetVideosForPlaylist(string plistId);
        YoutubePlaylist GetPlaylistForId(string playlistId);
    }
}