using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace tofix.YoutubeDataPuller
{
    public class YTDataPuller
    {
        private static string apiKey = ConfigurationManager.AppSettings["YoutubeAPIKey"];

        public static string ParseFromUrl(string url)
        {
            var queryString = url.Substring(url.IndexOf('?') + 1, url.Length - (url.IndexOf('?') + 1));
            var tokens = queryString.Split('&');
            foreach (var token in tokens)
            {
                if (token.StartsWith("v="))
                    return token.Substring(2);
            }
            throw new InvalidOperationException("Not a valid youtube URL");
        }

        public static async Task<SearchResultSnippet> GetVideoInfo(string url)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "Lighthouse"
            });

            var videoId = ParseFromUrl(url);

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = videoId; // Replace with your search term.
            searchListRequest.MaxResults = 5;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            var result = searchListResponse.Items.First().Snippet;

            return result;
        }

        //static async Task Main(string[] args)
        //{
        //    var result = await GetVideoInfo("https://www.youtube.com/watch?t=300s&v=eUuYgmWq9F4");
        //    Console.WriteLine($"{result.Title} 🍆 {result.Description} 🍆 {result.PublishedAt} 🍆 {result.ChannelTitle} 🍆 {result.Thumbnails.High.Url}");
        //}



    }
}