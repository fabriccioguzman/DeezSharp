using System;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerAlbumBase
    {
        [JsonProperty("id")]
        public int AlbumId { get; set; }

        [JsonProperty("title")]
        public string AlbumName { get; set; }

        [JsonProperty("link")]
        public string AlbumLink { get; set; }       //not set in search

        [JsonProperty("cover")]
        public string LinkCover { get; set; }

        [JsonProperty("cover_small")]
        public string LinkCoverSmall { get; set; }

        [JsonProperty("cover_medium")]
        public string LinkCoverMedium { get; set; }

        [JsonProperty("cover_big")]
        public string LinkCoverBig { get; set; }

        [JsonProperty("cover_xl")]
        public string LinkCoverXl { get; set; }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }  //not set in search


        [JsonProperty("tracklist")]
        internal string TrackListUrl { get; set; }

        [JsonProperty("type")]
        internal string Type { get; set; }
    }
}
