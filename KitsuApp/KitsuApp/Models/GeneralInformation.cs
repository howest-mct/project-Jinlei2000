using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace KitsuApp.Models
{
    public class GeneralInformation
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

        public string AiredDate
        {
            get
            {
                string start = "";
                string end = "";
                if (StartDate == null)
                {
                    start = "?";
                }
                else
                {
                    DateTime dateTime = DateTime.Parse(StartDate);
                    start = dateTime.ToString("MMM M yyyy");
                }

                if (EndDate == null)
                {
                    end = "?";
                }
                else
                {
                    DateTime dateTime = DateTime.Parse(EndDate);
                    end = dateTime.ToString("MMM M yyyy");
                }
                return $"{start} - {end}";
            }
        }

        public string AgeRatingString
        {
            get
            {
                if (AgeRatingGuide != null)
                {
                    return AgeRatingGuide;
                }
                else if (AgeRating != null)
                {
                    return AgeRating;
                }
                return "N/A";
            }
        }

        public float RatingProcent
        {
            get
            {
                if (AverageRating == null)
                {
                    return 0;
                }
                else
                {
                    // Convert the string to a float
                    float rating = float.Parse(AverageRating, CultureInfo.InvariantCulture.NumberFormat);
                    // Round to 2 decimal place and divide by 10 to get a rating out of 10
                    return (float)Math.Round(rating / 10, 2);
                }
            }
        }

        public string Season
        {
            get
            {
                if (StartDate == null) return "N/A";
                string season = "";
                DateTime date = DateTime.Parse(StartDate);
                float value = (float)date.Month + date.Day / 100f;  // <month>.<day(2 digit)>
                season = "Autumn";  // Autumn
                if (value < 9.23) season = "Summer"; // Summer
                if (value < 6.21) season = "Spring"; // Spring
                if (value < 3.21 || value >= 12.22) season = "Winter";   // Winter
                return $"{season} {date.Year}";
            }
        }

        public string StatusString
        {
            get
            {
                return Status.Substring(0, 1).ToUpper() + Status.Substring(1);
            }
        }

        public string SynopsisText
        {
            get
            {
                if (Synopsis == null)
                {
                    return "No Description";
                }
                return Synopsis;
            }
        }

        public string HighestRatedRankString
        {
            get
            {
                if (HighestRatedRank == null)
                {
                    return "#?";
                }
                return $"#{HighestRatedRank}";
            }
        }
        public string PopularityRankString
        {
            get
            {
                if (PopularityRank == null)
                {
                    return "#?";
                }
                return $"#{PopularityRank}";
            }
        }
        public string MemberCountString
        {
            get
            {
                if (Members == null)
                {
                    return "0";
                }
                return $"{Members}";
            }
        }



    }

    public class PosterImage
    {
        [JsonProperty(PropertyName = "small", NullValueHandling = NullValueHandling.Ignore)]
        public string Small { get; set; }

        [JsonProperty(PropertyName = "medium", NullValueHandling = NullValueHandling.Ignore)]
        public string Medium { get; set; }
    }
}
