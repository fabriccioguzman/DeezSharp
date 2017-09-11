using System;
using Newtonsoft.Json.Linq;

namespace DeezSharp
{
    public class DeezerSong
    {
	    public int SongId => _song.Value<int>("SNG_ID");

	    public int AlbumId => _song.Value<int>("ALB_ID");
	    public string AlbumPicture => _song.Value<string>("ALB_PICTURE");
	    public string AlbumTitle => _song.Value<string>("ALB_TITLE");

		//TODO: artists[]
	    public int ArtistId => _song.Value<int>("ART_ID");
	    public string ArtistName => _song.Value<string>("ART_NAME");

	    public string SongTitle => _song.Value<string>("SNG_TITLE");
	    public string Version => _song.Value<string>("VERSION");

		public float Bpm => _song.Value<float>("BPM");
		public int DiskNumber => _song.Value<int>("DISK_NUMBER");
	    public int Duration => _song.Value<int>("DURATION");
	    public float Gain => _song.Value<float>("GAIN");
	    public int GenreId => _song.Value<int>("GENRE_ID");
	    public DateTime ReleaseDateDigital => _song.Value<DateTime>("DIGITAL_RELEASE_DATE");
	    public DateTime ReleaseDatePhysical => _song.Value<DateTime>("PHYSICAL_RELEASE_DATE");
	    public int SongRank => _song.Value<int>("RANK_SNG");
	    public int TrackNumber => _song.Value<int>("TRACK_NUMBER");

	    internal string OriginMd5 => _song.Value<string>("MD5_ORIGIN");
	    internal int MediaVersion => _song.Value<int>("MEDIA_VERSION");

		private readonly JToken _song;

	    internal DeezerSong(JToken data)
	    {
		    _song = data;
	    }
    }
}
