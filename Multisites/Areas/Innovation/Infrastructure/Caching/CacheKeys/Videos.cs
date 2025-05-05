using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NM_MultiSites.Areas.Innovation.Infrastructure.Caching.CacheKeys
{
    public partial class CacheKeys
    {
        public class Videos
        {
            public class Youtube
            {
                public static string GetAllPlayLists()
                {
                    return "Videos/Youtube/GetAllPlayLists";
                }
                public static string GetCategory(string categoryId)
                {
                    return string.Format("Videos/Youtube/GetCategory/{0}", categoryId);
                }
                public static string GetPlaylistForId(string playlistId)
                {
                    return string.Format("Videos/Youtube/GetPlaylistForId/{0}", playlistId);
                }
                public static string GetVideosForIds(List<string> videoIds)
                {
                    return string.Format("Videos/Youtube/GetVideosForIds/{0}", string.Join("/", videoIds.OrderBy(x => x)));
                }
                public static string GetVideosForPlaylist(string plistId)
                {
                    return string.Format("Videos/Youtube/GetVideosForPlaylist/{0}", plistId);
                }
            }
        }
    }
}
