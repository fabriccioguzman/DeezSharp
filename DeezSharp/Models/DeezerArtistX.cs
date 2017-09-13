using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerArtistX
    {
        [JsonProperty("ART_ID")]
        public int ArtistId { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtistName { get; set; }

        [JsonProperty("RANK")]
        public int Rank { get; set; }


        [JsonProperty("ROLE_ID")]
        internal int RoleId { get; set; }

        [JsonProperty("ARTISTS_SONGS_ORDER")]
        internal int ArtistsSongsOrder { get; set; }

        [JsonProperty("ART_PICTURE")]
        internal string ArtPicture { get; set; }

        [JsonProperty("SMARTRADIO")]
        internal int SmartRadio { get; set; }

        [JsonProperty("__TYPE__")]
        internal string Type { get; set; }
    }
}
