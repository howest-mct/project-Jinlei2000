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
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Synopsis { get; set; }
        public string AverageRating { get; set; }
        public string startDate { get; set; }
        public string Subtype { get; set; }
        public string Status { get; set; }
        public string PosterImage { get; set; }

        public abstract string CollectionType { get; }

        [JsonExtensionData]
        private Dictionary<string, JToken> _attributes = new Dictionary<string, JToken>();

        [OnDeserialized]
        private void GetAttributes(StreamingContext context)
        {
            JToken attributesData = (JToken)_attributes["attributes"];
            Id = (string)attributesData.SelectToken("id");
            Slug = (string)attributesData.SelectToken("slug");
            Synopsis = (string)attributesData.SelectToken("synopsis");
            AverageRating = (string)attributesData.SelectToken("averageRating");
            startDate = (string)attributesData.SelectToken("startDate");
            Subtype = (string)attributesData.SelectToken("subtype");
            Status = (string)attributesData.SelectToken("status");
            PosterImage = (string)attributesData.SelectToken("posterImage");

        }


    }
}
