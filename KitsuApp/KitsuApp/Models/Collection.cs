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

        public abstract string CollectionType { get; }

    }

}
