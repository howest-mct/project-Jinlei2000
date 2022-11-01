using KitsuApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        
        public static async Task<List<Anime>> GetAnimesAsync()
        {
            string url = $"{_BASEURL}/anime";
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
                    throw ex;
                }
            }
        }


    }
}
