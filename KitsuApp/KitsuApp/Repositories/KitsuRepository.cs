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
            else if (type == "manhua") { extra = "&sort=popularityRank&filter[subtype]=manhua"; }
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

        // Get Genre by Anime ID
        public static async Task<List<Genre>> GetGenresFromAnimeIDAsync(string animeId)
        {
            string url = $"{_BASEURL}/anime/{animeId}/genres?page[limit]=100";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    JObject fullObject = JsonConvert.DeserializeObject<JObject>(json);
                    JToken data = fullObject.SelectToken("data");
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

        // Get Genre by Manga ID
        public static async Task<List<Genre>> GetGenresFromMangaIDAsync(string mangaId)
        {
            string url = $"{_BASEURL}/manga/{mangaId}/genres?page[limit]=100";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    JObject fullObject = JsonConvert.DeserializeObject<JObject>(json);
                    JToken data = fullObject.SelectToken("data");
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

        // Get your favorite Animes
        public static async Task<List<Anime>> GetFavoriteAnimesAsync()
        {
            string url = $"https://leijin.azurewebsites.net/api/favorites/anime";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    List<Anime> animes = JsonConvert.DeserializeObject<List<Anime>>(json);
                    return animes;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Get your favorite Mangas

        // Post your favorite Anime
        public static async Task PostFavoriteAnimeAsync(Anime anime)
        {
            string url = $"https://leijin.azurewebsites.net/api/favorites/anime";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(anime);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccessful Post to url: {url}, object: {json}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Post your favorite Manga

        // Put your favorite Anime
        public static async Task PutFavoriteAnimeAsync(Anime anime)
        {
            string url = $"https://leijin.azurewebsites.net/api/favorites/anime/{anime.Id}";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(anime);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccessful Put to url: {url}, object: {json}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }


        // Put your favorite Manga

        // Delete your favorite Anime
        public static async Task DeleteFavoriteAnimeAsync(string animeId)
        {
            string url = $"https://leijin.azurewebsites.net/api/favorites/anime/{animeId}";
            Debug.WriteLine(url);
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccessful Delete to url: {url}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    throw ex;
                }
            }
        }

        // Delete your favorite Manga

        // Get check if Anime or Manga is in favorites
        public static async Task<bool> GetCheckFavNotExists(string type, string id)
        {
            if (type == "anime")
            {
                List<Anime> animes = await GetFavoriteAnimesAsync();
                foreach (Anime anime in animes)
                {
                    if (anime.Id == id)
                    {
                        return true;
                    }
                }
            }
            //else if (type == "manga")
            //{
            //    List<Manga> mangas = await GetFavoriteMangasAsync();
            //    foreach (Manga manga in mangas)
            //    {
            //        if (manga.Id == id)
            //        {
            //            return true;
            //        }
            //    }
            //}
            return false;
        }
    }
}
