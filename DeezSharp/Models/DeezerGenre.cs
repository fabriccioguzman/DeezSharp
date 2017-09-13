using System;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerGenre : DeezerGenreBase
    {
        [JsonProperty("picture_small")]
        public string UrlPictureSmall { get; set; }

        [JsonProperty("picture_medium")]
        public string UrlPictureMedium { get; set; }

        [JsonProperty("picture_big")]
        public string UrlPictureBig { get; set; }

        [JsonProperty("picture_xl")]
        public string UrlPictureXl { get; set; }
    }
}
