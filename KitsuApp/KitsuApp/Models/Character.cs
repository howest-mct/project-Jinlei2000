using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuApp.Models
{
    public class Character : Collection
    {
        [JsonProperty(PropertyName = "attributes")]
        public CharacterInformation CharacterInfo { get; set; }
        public override string CollectionType => "character";
    }

    public class CharacterInformation
    {
        [JsonProperty(PropertyName = "slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
        public CharacterImage Image { get; set; }
    }

    public class CharacterImage
    {
        [JsonProperty(PropertyName = "original", NullValueHandling = NullValueHandling.Ignore)]
        public string Original { get; set; }
    }




}
