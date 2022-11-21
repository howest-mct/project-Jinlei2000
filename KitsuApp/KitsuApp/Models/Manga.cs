using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;

namespace KitsuApp.Models
{
    public class Manga : Collection
    {
        [JsonProperty(PropertyName = "attributes")]
        public MangaInformation MangaInfo { get; set; }

        public override string CollectionType => "manga";

        public string Name => MangaInfo.Name;

        public string PosterImageMedium => MangaInfo.PosterImage.Medium;
    }

    public class MangaInformation : GeneralInformation
    {
        [JsonProperty(PropertyName = "chapterCount", NullValueHandling = NullValueHandling.Ignore)]
        public int ChapterCount { get; set; }
        [JsonProperty(PropertyName = "volumeCount", NullValueHandling = NullValueHandling.Ignore)]
        public int VolumeCount { get; set; }

        public string TotalPages
        {
            get
            {
                if (ChapterCount == 0)
                {
                    return "N/A";
                }
                else if (VolumeCount == 0)
                {
                    return $"{ChapterCount} chapters";
                }
                else
                {
                    return $"{VolumeCount * ChapterCount} chapters";
                }
            }
        }

        public string ChapterCountString
        {
            get
            {
                if (ChapterCount == 0 || ChapterCount == null)
                {
                    return "N/A";
                }
                else
                {
                    return $"{ChapterCount}";
                }
            }
        }

        public string VolumeCountString
        {
            get
            {
                if (VolumeCount == 0 || VolumeCount == null)
                {
                    return "N/A";
                }
                else
                {
                    return $"{VolumeCount}";
                }
            }
        }
    }


}
