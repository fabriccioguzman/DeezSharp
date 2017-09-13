using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerContributor : DeezerArtistBase
    {
        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
