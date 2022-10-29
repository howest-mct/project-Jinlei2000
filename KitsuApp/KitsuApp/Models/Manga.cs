using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KitsuApp.Models
{
    public class Manga : Collection
    {
        public int ChapterCount { get; set; }
        public int VolumeCount { get; set; }
        public override string CollectionType => "manga";

        public string TotalPages
        {
            get
            {
                if (VolumeCount == 0 || VolumeCount == -1)
                {
                    return "N/A";
                }
                else if (ChapterCount == -1 && VolumeCount > 0)
                {
                    return $"{VolumeCount} pages";
                }
                else
                {
                    return $"{VolumeCount*ChapterCount} pages";
                }
            }
        }

        [JsonExtensionData]
        private Dictionary<string, JToken> _attributes = new Dictionary<string, JToken>();

        [OnDeserialized]
        private void GetAttributes(StreamingContext context)
        {
            JToken attributesData = (JToken)_attributes["attributes"];
            ChapterCount = (int)attributesData.SelectToken("chapterCount");
            VolumeCount = (int)attributesData.SelectToken("volumeCount");
        }
    }

}
