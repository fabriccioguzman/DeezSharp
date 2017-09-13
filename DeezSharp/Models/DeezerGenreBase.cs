using System;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerGenreBase
    {
        [JsonProperty("id")]
        public int GenreId { get; set; }

        [JsonProperty("name")]
        public string GenreName { get; set; }

        [JsonProperty("picture")]
        public string UrlPicture { get; set; }


        [JsonProperty("type")]
        internal string Type { get; set; }
    }
}
