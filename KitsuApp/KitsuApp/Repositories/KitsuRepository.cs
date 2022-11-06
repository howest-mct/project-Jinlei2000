using KitsuApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace KitsuApp.Repositories
{
    public static class KitsuRepository
    {
        private const string _BASEURL = "https://kitsu.io/api/edge";
        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Accept", "application/vnd.api+json");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/vnd.api+json");
            return client;
        }

        // Get Anime by amount & filter type (trending, popular, rated, favorite, updated, upcoming, movie)
        public static async Task<List<Anime>> GetAnimesAsync(int amount, string type)
        {
            string extra = "";
            if (type == "rated") { extra = "&sort=-averageRating"; }
            else if (type == "popular") { extra = "&sort=popularityRank"; }
            else if (type == "updated") { extra = "&sort=-updatedAt"; }
            else if (type == "favorite") { extra = "&sort=-favoritesCount"; }
            else if (type == "movie") { extra = "&sort=popularityRank&filter[subtype]=movie"; }
            else if (type == "upcoming") { extra = "&filter[status]=upcoming"; }

            string url = $"{_BASEURL}/anime?page[limit]={amount}{extra}";
            if (type == "trending") { url = $"{_BASEURL}/trending/anime?limit={amount}"; }

            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    //object deserializeren naar JObject (< newtonsoft)
                    JObject fullObject = JsonConvert.DeserializeObject<JObject>(json);
                    //Find token "data" in JObject
                    JToken data = fullObject.SelectToken("data");
                    //Convert JToken to List<Anime>
                    List<Anime> animes = data.ToObject<List<Anime>>();
                    return animes;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Get Manga by amount & filter type
        public static async Task<List<Manga>> GetMangasAsync(int amount, string type)
        {
            string extra = "";
            if (type == "rated") { extra = "&sort=-averageRating"; }
            else if (type == "popular") { extra = "&sort=popularityRank"; }
            else if (type == "updated") { extra = "&sort=-updatedAt"; }
            else if (type == "favorite") { extra = "&sort=-favoritesCount"; }
            else if (type == "movie") { extra = "&sort=popularityRank&filter[subtype]=movie"; }
            else if (type == "upcoming") { extra = "&filter[status]=upcoming"; }

            string url = $"{_BASEURL}/manga?page[limit]={amount}{extra}";
            if (type == "trending") { url = $"{_BASEURL}/trending/manga?limit={amount}"; }

            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    //object deserializeren naar JObject (< newtonsoft)
                    JObject fullObject = JsonConvert.DeserializeObject<JObject>(json);
                    //Find token "data" in JObject
                    JToken data = fullObject.SelectToken("data");
                    //Convert JToken to List<Manga>
                    List<Manga> mangas = data.ToObject<List<Manga>>();
                    return mangas;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Get all genres
        public static async Task<List<Genre>> GetGenresAsync()
        {
            string url = $"{_BASEURL}/genres?page[limit]=100";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    JObject fullObject = JsonConvert.DeserializeObject<JObject>(json);
                    JToken data = fullObject.SelectToken("data");
                    JToken links = fullObject.SelectToken("links");
                    List<Genre> genres = data.ToObject<List<Genre>>();
                    return genres;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Get all Anime from genre
        public static async Task<List<Anime>> GetAnimesFromGenreAsync(string genreName)
        {
            string url = $"{_BASEURL}/anime?page[limit]=20&filter[genres]={genreName}&sort=popularityRank";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    JObject fullObject = JsonConvert.DeserializeObject<JObject>(json);
                    JToken data = fullObject.SelectToken("data");
                    List<Anime> animes = data.ToObject<List<Anime>>();
                    return animes;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Get all Manga from genre
        public static async Task<List<Manga>> GetMangasFromGenreAsync(string genreName)
        {
            string url = $"{_BASEURL}/manga?page[limit]=20&filter[genres]={genreName}&sort=popularityRank";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    JObject fullObject = JsonConvert.DeserializeObject<JObject>(json);
                    JToken data = fullObject.SelectToken("data");
                    List<Manga> mangas = data.ToObject<List<Manga>>();
                    return mangas;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Get your favorite Animes

        // Get your favorite Mangas

        // Post your favorite Anime

        // Post your favorite Manga

        // Put your favorite Anime

        // Put your favorite Manga
    }
}
