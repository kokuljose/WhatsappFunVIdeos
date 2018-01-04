using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XamarinForms.Models;

namespace XamarinForms.ViewModels
{
    public class YoutubeViewModel : INotifyPropertyChanged
    {

        // use this link to get an api_key : https://console.developers.google.com/apis/api/youtube/
        private const string ApiKey = "AIzaSyD1uun5KInXR3tdLKI_o18zc1uBln_s5-E";


        // doc : https://developers.google.com/apis-explorer/#p/youtube/v3/youtube.playlistItems.list
        private string apiUrlForPlaylist = "https://www.googleapis.com/youtube/v3/playlistItems?part=contentDetails&maxResults=49&playlistId="
            + "PLAD9AA9B035E55CF3"
            //+ "Your_PlaylistId"
            + "&key="
            + ApiKey;
        private string apiUrlForPlaylist2 = "https://www.googleapis.com/youtube/v3/playlistItems?part=contentDetails&maxResults=49&playlistId="
            + "PLLMqJfXFbMrJiTkhmH2-XIoWRjap71wRN"
            //+ "Your_PlaylistId"
            + "&key="
            + ApiKey;

       // private string apiUrlForPlaylist;


        // doc : https://developers.google.com/apis-explorer/#p/youtube/v3/youtube.search.list
        private string apiUrlForVideosDetails = "https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&id="
            + "{0}"
            //+ "0ce22qhacyo,j8GU5hG-34I,_0aQJHoI1e8"
            //+ "Your_Videos_Id"
            + "&key="
            + ApiKey;

        private List<YoutubeItem> _youtubeItems;

        public List<YoutubeItem> YoutubeItems
        {
            get { return _youtubeItems; }
            set
            {
                _youtubeItems = value;
                OnPropertyChanged();
            }
        }

        public YoutubeViewModel()
        {
            InitDataAsync();
        }

        public async Task InitDataAsync()
        {

            //var videoIds = await GetVideoIdsFromChannelAsync();

            var videoIds = await GetVideoIdsFromPlaylistAsync();
        }

       
        private async Task<List<string>> GetVideoIdsFromPlaylistAsync()
        {

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(apiUrlForPlaylist);

            var videoIds = new List<string>();

            try
            {
                JObject response = JsonConvert.DeserializeObject<dynamic>(json);

                var items = response.Value<JArray>("items");

                foreach (var item in items)
                {
                    videoIds.Add(item.Value<JObject>("contentDetails")?.Value<string>("videoId"));
                };

                YoutubeItems = await GetVideosDetailsAsync(videoIds);
            }
            catch (Exception exception)
            {
            }

            return videoIds;
        }

        private async Task<List<YoutubeItem>> GetVideosDetailsAsync(List<string> videoIds)
        {

            var videoIdsString = "";
            foreach (var s in videoIds)
            {
                videoIdsString += s + ",";
            }

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(string.Format(apiUrlForVideosDetails, videoIdsString));

            var youtubeItems = new List<YoutubeItem>();

            try
            {
                JObject response = JsonConvert.DeserializeObject<dynamic>(json);

                var items = response.Value<JArray>("items");

                foreach (var item in items)
                {
                    var snippet = item.Value<JObject>("snippet");
                    var statistics = item.Value<JObject>("statistics");

                    var youtubeItem = new YoutubeItem
                    {
                        Title = snippet.Value<string>("title"),
                        VideoId = item?.Value<string>("id"),
                        MediumThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url"),
                       
                    };

                    youtubeItems.Add(youtubeItem);
                }

                return youtubeItems;
            }
            catch (Exception exception)
            {
                return youtubeItems;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}