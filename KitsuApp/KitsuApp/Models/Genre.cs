using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuApp.Models
{
    public class Genre : Collection
    {
        [JsonProperty(PropertyName = "attributes")]
        public GenreInformation GenreInfo { get; set; }

        public override string CollectionType => "genre";
    }

    public class GenreInformation
    {
        [JsonProperty(PropertyName = "slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
