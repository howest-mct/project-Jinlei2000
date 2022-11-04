using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KitsuApp.Models
{
    public abstract class Collection
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "favName", NullValueHandling = NullValueHandling.Ignore)]
        public string FavName { get; set; }
        //public string Slug { get; set; }
        //public string Synopsis { get; set; }
        //public string AverageRating { get; set; }
        //public string StartDate { get; set; }
        //public string Subtype { get; set; }
        //public string Status { get; set; }
        //public string PosterImage { get; set; }

        //[JsonProperty(PropertyName = "attributes")]
        //public GeneralInformation GeneralInfo { get; set; }

        public abstract string CollectionType { get; }

        //[JsonExtensionData]
        //private Dictionary<string, JToken> _extraJsonData = new Dictionary<string, JToken>();

        //[OnDeserialized]
        //private void ProcessExtraData(StreamingContext context)
        //{
        //    JToken attributesData = (JToken)_extraJsonData["attributes"];
        //    Slug = (string)attributesData.SelectToken("slug");
        //    Synopsis = (string)attributesData.SelectToken("synopsis");
        //    AverageRating = (string)attributesData.SelectToken("averageRating");
        //    startDate = (string)attributesData.SelectToken("startDate");
        //    Subtype = (string)attributesData.SelectToken("subtype");
        //    Status = (string)attributesData.SelectToken("status");
        //    PosterImage = (string)attributesData.SelectToken("posterImage");
        //}

    }
    //public class GeneralInformation
    //{
    //    [JsonProperty(PropertyName = "slug", NullValueHandling = NullValueHandling.Ignore)]
    //    public string Slug { get; set; }
    //    [JsonProperty(PropertyName = "synopsis", NullValueHandling = NullValueHandling.Ignore)]
    //    public string Synopsis { get; set; }
    //    [JsonProperty(PropertyName = "averageRating", NullValueHandling = NullValueHandling.Ignore)]
    //    public string AverageRating { get; set; }
    //    [JsonProperty(PropertyName = "startDate", NullValueHandling = NullValueHandling.Ignore)]
    //    public string StartDate { get; set; }
    //    [JsonProperty(PropertyName = "subtype", NullValueHandling = NullValueHandling.Ignore)]
    //    public string Subtype { get; set; }
    //    [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
    //    public string Status { get; set; }
    //    [JsonProperty(PropertyName = "posterImage", NullValueHandling = NullValueHandling.Ignore)]
    //    public string PosterImage { get; set; }
    //}

}
