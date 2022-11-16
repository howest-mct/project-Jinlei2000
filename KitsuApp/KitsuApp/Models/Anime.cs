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
        //public int EpisodeCount { get; set; }
        //public int EpisodeLength { get; set; }
        //public int TotalLength { get; set; }
        [JsonProperty(PropertyName = "attributes")]
        public AnimeInformation AnimeInfo { get; set; }

        public override string CollectionType => "anime";

        public string TotalTime
        {
            get
            {
                int TotalMinutes = 0;
                if (AnimeInfo.EpisodeLength == 0 && AnimeInfo.TotalLength == 0)
                {
                    return "N/A";
                }
                else if (AnimeInfo.TotalLength > 0)
                {
                    TotalMinutes = AnimeInfo.TotalLength;
                }
                else
                {
                    TotalMinutes = AnimeInfo.EpisodeLength * AnimeInfo.EpisodeCount;
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

        public string Aired
        {
            get
            {
                string start = "";
                string end = "";
                if (AnimeInfo.StartDate == null)
                {
                    start = "?";
                }
                else
                {
                    DateTime dateTime = DateTime.Parse(AnimeInfo.StartDate);
                    start = dateTime.ToString("MMM M yyyy");
                }

                if (AnimeInfo.EndDate == null)
                {
                    end = "?";
                }
                else
                {
                    DateTime dateTime = DateTime.Parse(AnimeInfo.EndDate);
                    end = dateTime.ToString("MMM M yyyy");
                }
                return $"{start} - {end}";
            }
        }

        public string AgeRating
        {
            get
            {
                if (AnimeInfo.AgeRatingGuide != null)
                {
                    return AnimeInfo.AgeRatingGuide;
                }
                else if (AnimeInfo.AgeRating != null)
                {
                    return AnimeInfo.AgeRating;
                }
                return "N/A";
            }
        }

        public float Rating
        {
            get
            {
                if (AnimeInfo.AverageRating == null)
                {
                    return 0;
                }
                else
                {
                    // Convert the string to a float
                    float rating =  float.Parse(AnimeInfo.AverageRating, CultureInfo.InvariantCulture.NumberFormat);
                    // Round to 2 decimal place and divide by 10 to get a rating out of 10
                    return (float)Math.Round(rating / 10, 2);
                }
            }
        }

        public string TrailerLink
        {
            get
            {
                if (AnimeInfo.YoutubeVideoId == null)
                {
                    return "N/A";
                }
                else
                {
                    return $"https://www.youtube.com/watch?v={AnimeInfo.YoutubeVideoId}";
                }
            }
        }

        public string Season
        {
            get
            {
                if (AnimeInfo.StartDate == null) return "N/A";
                string season = "";
                DateTime date = DateTime.Parse(AnimeInfo.StartDate);
                float value = (float)date.Month + date.Day / 100f;  // <month>.<day(2 digit)>
                season = "Autumn";  // Autumn
                if (value < 9.23) season = "Summer"; // Summer
                if (value < 6.21) season = "Spring"; // Spring
                if (value < 3.21 || value >= 12.22) season = "Winter";   // Winter
                return $"{season} {date.Year}";
            }
        }

        //[JsonExtensionData]
        //private Dictionary<string, JToken> _attributes = new Dictionary<string, JToken>();

        //[OnDeserialized]
        //private void GetAttributes(StreamingContext context)
        //{
        //    JToken attributesData = (JToken)_attributes["attributes"];
        //    EpisodeCount = (int)attributesData.SelectToken("episodeCount");
        //    EpisodeLength = (int)attributesData.SelectToken("episodeLength");
        //    TotalLength = (int)attributesData.SelectToken("totalLength");
        //}

    }

    public class AnimeInformation
    {
        [JsonProperty(PropertyName = "slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }
        [JsonProperty(PropertyName = "canonicalTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "synopsis", NullValueHandling = NullValueHandling.Ignore)]
        public string Synopsis { get; set; }
        [JsonProperty(PropertyName = "averageRating", NullValueHandling = NullValueHandling.Ignore)]
        public string AverageRating { get; set; }
        [JsonProperty(PropertyName = "startDate", NullValueHandling = NullValueHandling.Ignore)]
        public string StartDate { get; set; }
        [JsonProperty(PropertyName = "endDate", NullValueHandling = NullValueHandling.Ignore)]
        public string EndDate { get; set; }
        [JsonProperty(PropertyName = "subtype", NullValueHandling = NullValueHandling.Ignore)]
        public string Subtype { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "posterImage", NullValueHandling = NullValueHandling.Ignore)]
        public PosterImage PosterImage { get; set; }


        [JsonProperty(PropertyName = "userCount", NullValueHandling = NullValueHandling.Ignore)]
        public string Members { get; set; }
        [JsonProperty(PropertyName = "ratingRank", NullValueHandling = NullValueHandling.Ignore)]
        public string HighestRatedRank { get; set; }
        [JsonProperty(PropertyName = "popularityRank", NullValueHandling = NullValueHandling.Ignore)]
        public string PopularityRank { get; set; }
        [JsonProperty(PropertyName = "ageRating", NullValueHandling = NullValueHandling.Ignore)]
        public string AgeRating { get; set; }
        [JsonProperty(PropertyName = "ageRatingGuide", NullValueHandling = NullValueHandling.Ignore)]
        public string AgeRatingGuide { get; set; }
        [JsonProperty(PropertyName = "youtubeVideoId", NullValueHandling = NullValueHandling.Ignore)]
        public string YoutubeVideoId { get; set; }

        [JsonProperty(PropertyName = "episodeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int EpisodeCount { get; set; }
        [JsonProperty(PropertyName = "episodeLength", NullValueHandling = NullValueHandling.Ignore)]
        public int EpisodeLength { get; set; }
        [JsonProperty(PropertyName = "totalLength", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalLength { get; set; }
    }
    public class PosterImage
    {
        [JsonProperty(PropertyName = "small", NullValueHandling = NullValueHandling.Ignore)]
        public string Small { get; set; }

        [JsonProperty(PropertyName = "medium", NullValueHandling = NullValueHandling.Ignore)]
        public string Medium { get; set; }

    }
}
