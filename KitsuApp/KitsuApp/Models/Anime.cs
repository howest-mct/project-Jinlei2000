using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;

namespace KitsuApp.Models
{
    public class Anime : Collection
    {
        [JsonProperty(PropertyName = "attributes")]
        public AnimeInformation AnimeInfo { get; set; }

        public override string CollectionType => "anime";

        public string Name => AnimeInfo.Name;

        public string PosterImageMedium => AnimeInfo.PosterImage.Medium;
    }

    public class AnimeInformation : GeneralInformation
    {
        [JsonProperty(PropertyName = "youtubeVideoId", NullValueHandling = NullValueHandling.Ignore)]
        public string YoutubeVideoId { get; set; }
        [JsonProperty(PropertyName = "episodeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int EpisodeCount { get; set; }
        [JsonProperty(PropertyName = "episodeLength", NullValueHandling = NullValueHandling.Ignore)]
        public int EpisodeLength { get; set; }
        [JsonProperty(PropertyName = "totalLength", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalLength { get; set; }

        public string TotalTime
        {
            get
            {
                int TotalMinutes = 0;
                if (EpisodeLength == 0 && TotalLength == 0)
                {
                    return "N/A";
                }
                else if (TotalLength > 0)
                {
                    TotalMinutes = TotalLength;
                }
                else
                {
                    TotalMinutes = EpisodeLength * EpisodeCount;
                }
                int minutes = TotalMinutes % 60;
                int hours = (TotalMinutes - 20) / 60;
                if (minutes == 0)
                {
                    return $"{hours}h";
                }
                return $"{hours}h {minutes}m";
            }
        }
        public string TrailerLink
        {
            get
            {
                if (YoutubeVideoId == null)
                {
                    return "N/A";
                }
                else
                {
                    return $"https://www.youtube.com/watch?v={YoutubeVideoId}";
                }
            }
        }
        public string EpisodeCountString
        {
            get
            {
                if (EpisodeCount == 0 || EpisodeCount == 1 || EpisodeCount == null)
                {
                    return "N/A";
                }
                else
                {
                    return $"{EpisodeCount} episodes";
                }
            }
        }
        public string EpisodeLengthString
        {
            get
            {
                if (EpisodeLength == 0 || EpisodeLength == 1 || EpisodeLength == null)
                {
                    return "N/A";
                }
                else
                {
                    if (Subtype == "movie")
                    {
                        return $"{EpisodeLength} m";
                    }
                    else
                    {
                        return $"{EpisodeLength} m per ep.";
                    }
                }
            }
        }
    }

}
