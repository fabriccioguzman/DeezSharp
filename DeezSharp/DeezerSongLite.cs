using System;
using Newtonsoft.Json.Linq;

namespace DeezSharp
{
    public class DeezerSongLite
    {
        public int SongId => _song.Value<int>("id");
        public string Title => _song.Value<string>("title");
        public string TitleShort => _song.Value<string>("title_short");     //title minus version
        public string TitleVersion => _song.Value<string>("title_version"); //title minus actual title
        public string ISRC => _song.Value<string>("isrc");
        public string Link => _song.Value<string>("link");
        public int Duration => _song.Value<int>("duration");
        public int TrackPosition => _song.Value<int>("track_position");
        public int DiskNumber => _song.Value<int>("disk_number");
        public int Rank => _song.Value<int>("rank");
        public bool ExplicitLyrics => _song.Value<bool>("explicit_lyrics");
        public string PreviewUrl => _song.Value<string>("preview");
        public DeezerSongLite Alternative => _song.Value<DeezerSongLite>("alternative");
        //TODO: artist
        
        internal bool Readable => _song.Value<bool>("readable");    //may have to be made public
        internal string Type => _song.Value<string>("track");

        private readonly JToken _song;

        internal DeezerSongLite(JToken data)
        {
            _song = data;
        }
    }
}
