using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerAlbum : DeezerAlbumBase
    {
        [JsonProperty("genre_id")]
        public int GenreId { get; set; }
        
        [JsonIgnore]
        public DeezerGenreBase[] Genres => _genres.Genres.ToArray();
        [JsonProperty("genres")] private GenresData _genres;

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("nb_tracks")]
        public int AmountOfTracks { get; set; }

        [JsonProperty("duration")]
        public int TotalLength { get; set; }

        [JsonProperty("fans")]
        public int Fans { get; set; }

        [JsonProperty("record_type")]
        public string RecordType { get; set; }

        [JsonProperty("explicit_lyrics")]
        public bool ExplicitLyrics { get; set; }

        [JsonProperty("contributors")]
        public DeezerContributor[] Contributors { get; set; }

        [JsonProperty("artist")]
        public DeezerArtistBase Artist { get; set; }
        
        [JsonIgnore]
        public DeezerSong[] Tracks => _tracks.Tracks.ToArray();
        [JsonProperty("tracks")] private TracksData _tracks;


        [JsonProperty("rating")]
        internal float Rating { get; set; }     //prob not used anywhere

        [JsonProperty("available")]
        internal bool Available { get; set; }   //available isn't used by us

        private class TracksData
        {
            [JsonProperty("data")]
            public IEnumerable<DeezerSong> Tracks { get; set; }
        }
        private class GenresData
        {
            [JsonProperty("data")]
            public IEnumerable<DeezerGenreBase> Genres { get; set; }
        }
    }
}
