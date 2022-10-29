using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitsuApp.Repositories
{
    public class KitsuRepository
    {
        private const string _BASEURL = "https://kitsu.io/api/edge/";
        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.api+json");
            client.DefaultRequestHeaders.Add("Content-Type", "application/vnd.api+json");
            return client;
        }

        //public static async Task<List<Anime>> GetAnimesAsync()
        //{
        //    return null;
        //}
    }
}
