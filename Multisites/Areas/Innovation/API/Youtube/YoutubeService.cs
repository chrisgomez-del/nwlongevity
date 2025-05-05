using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using NM_MultiSites.Areas.Innovation;
using NM_MultiSites.Areas.Innovation.Infrastructure.Caching.CacheKeys;
using NM_MultiSites.Areas.Innovation.Infrastructure.Logging;
using NM_MultiSites.Areas.Innovation.API.Youtube.Interfaces;
using NM_MultiSites.Areas.Innovation.API.Youtube.Models;
using Sitecore.Configuration;

namespace NM_MultiSites.Areas.Innovation.API.Youtube
{
    public class YoutubeService : BaseService, IYoutubeService
    {
        private YouTubeService youtubeService;
        private string channelId;

        public YoutubeService(string apiKey, string channelId)
        {
            this.channelId = channelId;
            this.youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });
        }

        public static YoutubeService CreateDefault()
        {
            string ytApiKey = Settings.GetSetting("YoutubeApiKey");
            string channelId = Settings.GetSetting("YoutubeChannelId");

            return new YoutubeService(ytApiKey, channelId);
        }

        public virtual List<YoutubeVideo> GetVideosForPlaylist(string plistId)
        {
            return Cache.GetOrCreate(CacheKeys.Videos.Youtube.GetVideosForPlaylist(plistId),
                () =>
                {
                    var plistItemsRequest = this.youtubeService.PlaylistItems.List("snippet");
                    plistItemsRequest.PlaylistId = plistId;
                    plistItemsRequest.MaxResults = 50;

                    var videos = new List<YoutubeVideo>();

                    try
                    {
                        DebugLogger.LogMessage(string.Format("Calling Youtube Video Playlist Request - ID: {0}", plistId), "YoutubeService.GetVideosForPlaylist");
                        var response = plistItemsRequest.Execute();

                        var ids = response.Items
                            .Select(result => result.Snippet.ResourceId.VideoId)
                            .ToList();

                        videos = this.GetVideosForIds(ids);
                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error(string.Format("Failed Youtube Video Playlist Request - ID: {0}", plistId), ex, "YoutubeService.GetVideosForPlaylist");
                    }

                    return videos;
                });
        }

        /*
        public virtual List<YoutubeVideo> GetVideosForCategory(YoutubeCategory youtubeCategory)
        {
            List<YoutubeVideo> catVids =
                this.GetAllVideos()
                .Where(v => v.Category.Name == youtubeCategory.Name)
                .ToList();

            return catVids;
        }
        */

        public virtual YoutubeCategory GetCategory(string categoryId)
        {
            return Cache.GetOrCreate(CacheKeys.Videos.Youtube.GetCategory(categoryId),
                () =>
                {
                    var catRequest = this.youtubeService.VideoCategories.List("snippet");
                    catRequest.Id = categoryId;

                    DebugLogger.LogMessage(string.Format("Calling Youtube Category Request - ID: {0}", categoryId), "YoutubeService.GetCategory");
                    var response = catRequest.Execute();

                    var cat = response.Items
                        .Select(item => new YoutubeCategory(item, this))
                        .FirstOrDefault();

                    return cat;
                });
        }

        public virtual YoutubeVideo GetVideoForId(string videoId)
        {
            return this.GetVideosForIds(new List<string>() { videoId }).FirstOrDefault();
        }

        public virtual List<YoutubeVideo> GetVideosForIds(List<string> videoIds)
        {
            return Cache.GetOrCreate(CacheKeys.Videos.Youtube.GetVideosForIds(videoIds),
                () =>
                {
                    var videosRequest = this.youtubeService.Videos.List("snippet,contentDetails");
                    videosRequest.Id = string.Join(",", videoIds);

                    var videos = new List<YoutubeVideo>();

                    try
                    {
                        DebugLogger.LogMessage(string.Format("Calling Youtube Videos Request - IDs: {0}", videoIds.ToString()), ".YoutubeService.GetVideosForIds");
                        var response = videosRequest.Execute();

                        videos = response.Items
                                .Select(item => new YoutubeVideo(item))
                                .ToList();
                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error(string.Format("Failed Youtube Video Playlist Request - IDs: {0}", videoIds.ToString()), ex, ".YoutubeService.GetVideosForIds");
                    }

                    return videos;
                });
        }
        public virtual YoutubeVideo GetVideoForSingleId(string videoId)
        {
            var videosRequest = this.youtubeService.Videos.List("snippet,contentDetails");
                    videosRequest.Id = videoId;

                    var videos = new List<YoutubeVideo>();

                    try
                    {
                        DebugLogger.LogMessage(string.Format("Calling Youtube Video Request - ID: {0}", videoId), ".YoutubeService.GetVideosForIds");
                        var response = videosRequest.Execute();

                        videos = response.Items
                                .Select(item => new YoutubeVideo(item))
                                .ToList();
                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error(string.Format("Failed Youtube Video Playlist Request - IDs: {0}", videoId.ToString()), ex, ".YoutubeService.GetVideosForIds");
                    }

                    return videos.FirstOrDefault();
        }

        public virtual List<YoutubePlaylist> GetAllPlayLists()
        {
            return Cache.GetOrCreate(CacheKeys.Videos.Youtube.GetAllPlayLists(),
                () =>
                {
                    var searchPlaylistRequest = this.youtubeService.Playlists.List("snippet");
                    searchPlaylistRequest.ChannelId = this.channelId;
                    searchPlaylistRequest.MaxResults = 50;

                    var playlists = new List<YoutubePlaylist>();

                    try
                    {
                        DebugLogger.LogMessage("Calling Youtube All Video Playlist Request", ".YoutubeService.GetAllPlayLists");
                        var response = searchPlaylistRequest.Execute();

                        foreach (var searchResult in response.Items)
                        {
                            playlists.Add(new YoutubePlaylist(searchResult));
                        }

                    }
                    catch (Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error("Failed Youtube Video Playlist Request", ex, ".YoutubeService.GetAllPlayLists");
                    }

                    return playlists;
                });
        }

        public virtual YoutubePlaylist GetPlaylistForId(string playlistId)
        {
            return Cache.GetOrCreate(CacheKeys.Videos.Youtube.GetPlaylistForId(playlistId),
                () =>
                {
                    var searchPlaylistRequest = this.youtubeService.Playlists.List("snippet");
                    searchPlaylistRequest.Id = playlistId;

                    DebugLogger.LogMessage(string.Format("Calling Youtube Video Playlist Request - ID: {0}", playlistId), "YoutubeService.GetPlaylistForId");
                    var response = searchPlaylistRequest.Execute();

                    return response.Items
                        .Select(searchResult => new YoutubePlaylist(searchResult))
                        .FirstOrDefault();
                });
        }

        /*
        public virtual List<YoutubeVideo> GetAllVideos()
        {
            var searchListRequest = this.youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = this.channelId;
            searchListRequest.MaxResults = 50;

            var searchListResponse = searchListRequest.Execute();

            var videoIds = searchListResponse.Items
                .Where(item => item.Id.Kind == "youtube#video")
                .Select(item => item.Id.VideoId)
                .ToList();

            var videos = this.GetVideosForIds(videoIds);
            
            return videos;
        }
        */
    }
}