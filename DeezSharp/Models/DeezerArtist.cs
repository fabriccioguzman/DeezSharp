using System;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerArtist : DeezerArtistBase
    {
        [JsonProperty("nb_album")]
        public int AmountOfAlbums { get; set; }

        [JsonProperty("nb_fan")]
        public int AmountOfFans { get; set; }
    }
}
