using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuApp.Models
{
    public class Episode : Collection
    {
        [JsonProperty(PropertyName = "attributes")]
        public EpisodeInformation EpisodeInfo { get; set; }

        public override string CollectionType => "episode";

    }
    
    public class EpisodeInformation
    {
        [JsonProperty(PropertyName = "canonicalTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "thumbnail", NullValueHandling = NullValueHandling.Ignore)]
        public EpisodeImage Image { get; set; }
    }

    public class EpisodeImage
    {
        [JsonProperty(PropertyName = "original", NullValueHandling = NullValueHandling.Ignore)]
        public string Original { get; set; }
    }

}
