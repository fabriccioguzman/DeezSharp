using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerAlbum
    {
        [JsonProperty("id")]
        public int AlbumId { get; set; }

        [JsonProperty("title")]
        public string AlbumName { get; set; }

        [JsonProperty("link")]
        public string AlbumLink { get; set; }

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

        [JsonProperty("genre_id")]
        public int GenreId { get; set; }

        //TODO: genre[]

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("nb_tracks")]
        public int AmountOfTracks { get; set; }

        [JsonProperty("duration")]
        public int TotalLength { get; set; }

        [JsonProperty("fans")]
        public int Fans { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("record_type")]
        public string RecordType { get; set; }

        [JsonProperty("explicit_lyrics")]
        public bool ExplicitLyrics { get; set; }

        //TODO: contributors[]
        //TODO: artist

        //TODO make sure this works
        [JsonIgnore]
        public IEnumerable<DeezerSongLite> Tracks => _tracks.Tracks;
        [JsonProperty("tracks")] private TracksData _tracks;

        [JsonProperty("rating")]
        internal float Rating { get; set; }     //prob not used anywhere

        [JsonProperty("available")]
        internal bool Available { get; set; }   //available isn't used by us

        [JsonProperty("tracklist")]
        internal string TrackListUrl { get; set; }  //we could use this, but `tracks` already provides this info

        [JsonProperty("type")]
        internal string Type { get; set; }      //unused

        private class TracksData
        {
            [JsonProperty("data")]
            public IEnumerable<DeezerSongLite> Tracks { get; set; }
        }
    }
}
