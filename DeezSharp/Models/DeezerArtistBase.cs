using System;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerArtistBase
    {
        [JsonProperty("id")]
        public int ArtistId { get; set; }

        [JsonProperty("name")]
        public string ArtistName { get; set; }

        [JsonProperty("link")]
        public string Url { get; set; }

        [JsonProperty("picture")]
        public string AlbumArt { get; set; }

        [JsonProperty("picture_small")]
        public string AlbumArtSmall { get; set; }

        [JsonProperty("picture_medium")]
        public string AlbumArtMedium { get; set; }

        [JsonProperty("picture_big")]
        public string AlbumArtBig { get; set; }

        [JsonProperty("picture_xl")]
        public string AlbumArtXL { get; set; }


        [JsonProperty("share")]
        internal string LinkShare { get; set; }

        [JsonProperty("radio")]
        internal bool Radio { get; set; }

        [JsonProperty("tracklist")]
        internal string TrackList { get; set; }
    }
}
