using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace DeezSharp
{
    public class DeezerAlbum
    {
        public int AlbumId => _album.Value<int>("id");
        public string AlbumName => _album.Value<string>("title");
        public string AlbumLink => _album.Value<string>("link");

        public string LinkCover => _album.Value<string>("cover");
        public string LinkCoverSmall => _album.Value<string>("cover_small");
        public string LinkCoverMedium => _album.Value<string>("cover_medium");
        public string LinkCoverBig => _album.Value<string>("cover_big");
        public string LinkCoverXl => _album.Value<string>("cover_xl");

        public int GenreId => _album.Value<int>("genre_id");
        //TODO: genre[]
        public string Label => _album.Value<string>("label");
        public int AmountOfTracks => _album.Value<int>("nb_tracks");
        public int TotalLength => _album.Value<int>("duration");
        public int Fans => _album.Value<int>("fans");
        public DateTime ReleaseDate => _album.Value<DateTime>("release_date");
        public string RecordType => _album.Value<string>("record_type");
        public bool ExplicitLyrics => _album.Value<bool>("explicit_lyrics");

        //TODO: contributors[]
        //TODO: artist
        public IEnumerable<DeezerSongLite> Tracks => _album["tracks"]["data"].Children().Select(a => new DeezerSongLite(a));

        internal float Rating => _album.Value<float>("rating");     //prob not used anywhere
        internal bool Available => _album.Value<bool>("available"); //available isn't used by us
        internal string TrackListUrl => _album.Value<string>("tracklist");  //we could use this, but `tracks` already provides this info
        internal string Type => _album.Value<string>("type");       //unused

        private readonly JToken _album;

        internal DeezerAlbum(JToken data)
        {
            _album = data;
        }
    }
}
