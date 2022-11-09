using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;

namespace KitsuApp.Models
{
    public class Anime : Collection
    {
        //public int EpisodeCount { get; set; }
        //public int EpisodeLength { get; set; }
        //public int TotalLength { get; set; }
        [JsonProperty(PropertyName = "attributes")]
        public AnimeInformation AnimeInfo { get; set; }

        public override string CollectionType => "anime";

        public string TotalTime
        {
            get
            {
                int TotalMinutes = 0;
                if (AnimeInfo.EpisodeLength == 0 || AnimeInfo.TotalLength == 0)
                {
                    return "N/A";
                }
                else if (AnimeInfo.TotalLength > 0)
                {
                    TotalMinutes = AnimeInfo.TotalLength;
                }
                else
                {
                    TotalMinutes = AnimeInfo.EpisodeLength * AnimeInfo.EpisodeCount;
                }
                int minutes = TotalMinutes % 60;
                int hours = (TotalMinutes - 20) / 60;
                return $"{hours}h {minutes}m";
            }
        }

        public float Rating
        {
            get
            {
                if (AnimeInfo.AverageRating == null)
                {
                    return 0;
                }
                else
                {
                    //round decimal to 2 decimal places
                    return (float)Math.Round(float.Parse(AnimeInfo.AverageRating) / 10, 2);
                }
            }
        }

        //[JsonExtensionData]
        //private Dictionary<string, JToken> _attributes = new Dictionary<string, JToken>();

        //[OnDeserialized]
        //private void GetAttributes(StreamingContext context)
        //{
        //    JToken attributesData = (JToken)_attributes["attributes"];
        //    EpisodeCount = (int)attributesData.SelectToken("episodeCount");
        //    EpisodeLength = (int)attributesData.SelectToken("episodeLength");
        //    TotalLength = (int)attributesData.SelectToken("totalLength");
        //}

    }

    public class AnimeInformation
    {
        [JsonProperty(PropertyName = "slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }
        [JsonProperty(PropertyName = "canonicalTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "synopsis", NullValueHandling = NullValueHandling.Ignore)]
        public string Synopsis { get; set; }
        [JsonProperty(PropertyName = "averageRating", NullValueHandling = NullValueHandling.Ignore)]
        public string AverageRating { get; set; }
        [JsonProperty(PropertyName = "startDate", NullValueHandling = NullValueHandling.Ignore)]
        public string StartDate { get; set; }
        [JsonProperty(PropertyName = "subtype", NullValueHandling = NullValueHandling.Ignore)]
        public string Subtype { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "posterImage", NullValueHandling = NullValueHandling.Ignore)]
        public PosterImage PosterImage { get; set; }

        [JsonProperty(PropertyName = "episodeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int EpisodeCount { get; set; }
        [JsonProperty(PropertyName = "episodeLength", NullValueHandling = NullValueHandling.Ignore)]
        public int EpisodeLength { get; set; }
        [JsonProperty(PropertyName = "totalLength", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalLength { get; set; }
    }
    public class PosterImage
    {
        [JsonProperty(PropertyName = "small", NullValueHandling = NullValueHandling.Ignore)]

        public string Small { get; set; }

        [JsonProperty(PropertyName = "medium", NullValueHandling = NullValueHandling.Ignore)]
        public string Medium { get; set; }

    }
}
