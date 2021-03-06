﻿using System;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerSong
    {
        [JsonProperty("id")]
        public int SongId { get; set; }

        [JsonProperty("title")]
        public string SongTitle { get; set; }

        [JsonProperty("title_short")]
        public string SongTitleShort { get; set; }  //title minus version

        [JsonProperty("title_version")]
        public string SongTitleVersion { get; set; }//title minus actual title

        [JsonProperty("isrc")]
        public string ISRC { get; set; }        //not set in search

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("track_position")]
        public int? TrackPosition { get; set; } //not set in search

        [JsonProperty("disk_number")]
        public int? DiskNumber { get; set; }    //not set in search

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("explicit_lyrics")]
        public bool ExplicitLyrics { get; set; }

        [JsonProperty("preview")]
        public string PreviewUrl { get; set; }

        [JsonProperty("alternative")]
        public DeezerSong Alternative { get; set; } //not set in search

        [JsonProperty("bpm")]
        public int? Bpm { get; set; }               //not set in search

        [JsonProperty("gain")]
        public float? Gain { get; set; }            //not set in search

        [JsonProperty("contributors")]
        public DeezerContributor[] Contributors { get; set; }

        [JsonProperty("artist")]
        public DeezerArtistBase Artist { get; set; }

        [JsonProperty("album")]
        public DeezerAlbumBase Album { get; set; }

        [JsonProperty("readable")]
        internal bool Readable { get; set; }    //may have to be made public

        [JsonProperty("track")]
        internal string Type { get; set; }
    }
}
