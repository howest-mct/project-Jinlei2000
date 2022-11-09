using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;

namespace KitsuApp.Models
{
    public class Manga : Collection
    {
        //public int ChapterCount { get; set; }
        //public int VolumeCount { get; set; }
        public override string CollectionType => "manga";

        [JsonProperty(PropertyName = "attributes")]
        public MangaInformation MangaInfo { get; set; }

        public string TotalPages
        {
            get
            {
                if (MangaInfo.ChapterCount == 0)
                {
                    return "N/A";
                }
                else if (MangaInfo.VolumeCount == 0)
                {
                    return $"{MangaInfo.ChapterCount} total chapters";
                }
                else
                {
                    return $"{MangaInfo.VolumeCount * MangaInfo.ChapterCount} total chapters";
                }
            }
        }

        public float Rating
        {
            get
            {
                if (MangaInfo.AverageRating == null)
                {
                    return 0;
                }
                else
                {
                    //round decimal to 2 decimal places
                    return (float)Math.Round(float.Parse(MangaInfo.AverageRating) / 10, 2);
                }
            }
        }

        //[JsonExtensionData]
        //private Dictionary<string, JToken> _attributes = new Dictionary<string, JToken>();

        //[OnDeserialized]
        //private void GetAttributes(StreamingContext context)
        //{
        //    JToken attributesData = (JToken)_attributes["attributes"];
        //    ChapterCount = (int)attributesData.SelectToken("chapterCount");
        //    VolumeCount = (int)attributesData.SelectToken("volumeCount");
        //}
    }

    public class MangaInformation
    {
        [JsonProperty(PropertyName = "slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }
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
        public Image PosterImage { get; set; }
        [JsonProperty(PropertyName = "chapterCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ChapterCount { get; set; }
        [JsonProperty(PropertyName = "volumeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int VolumeCount { get; set; }
    }


}
