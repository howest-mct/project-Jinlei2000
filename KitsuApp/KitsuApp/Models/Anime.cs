using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KitsuApp.Models
{
    public class Anime : Collection
    {
        public int EpisodeCount { get; set; }
        public int EpisodeLength { get; set; }
        public int TotalLength { get; set; }

        public override string CollectionType => "anime";

        public string TotalTime
        {
            get
            {
                int TotalMinutes = 0;
                if (EpisodeLength == -1 || EpisodeLength == 0 || TotalLength == -1)
                {
                    return "N/A";
                }
                else if (EpisodeCount > 0 && EpisodeLength > 0)
                {
                    TotalMinutes = EpisodeLength * EpisodeCount;
                }
                else
                {
                    TotalMinutes = TotalLength;
                }
                int minutes = TotalMinutes % 60;
                int hours = (TotalMinutes-20) / 60;
                return $"{hours}h {minutes}m";

            }
        }

        [JsonExtensionData]
        private Dictionary<string, JToken> _attributes = new Dictionary<string, JToken>();

        [OnDeserialized]
        private void GetAttributes(StreamingContext context)
        {
            JToken attributesData = (JToken)_attributes["attributes"];
            EpisodeCount = (int)attributesData.SelectToken("episodeCount");
            EpisodeLength = (int)attributesData.SelectToken("episodeLength");
            TotalLength = (int)attributesData.SelectToken("totalLength");
        }
    }
}
