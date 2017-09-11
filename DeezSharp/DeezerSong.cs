using System;
using Newtonsoft.Json.Linq;

namespace DeezSharp
{
    public class DeezerSong
    {
	    public int SongId => _songData.Value<int>("SNG_ID");

	    public int AlbumId => _songData.Value<int>("ALB_ID");
	    public string AlbumPicture => _songData.Value<string>("ALB_PICTURE");
	    public string AlbumTitle => _songData.Value<string>("ALB_TITLE");

		//TODO: artists[]
	    public int ArtistId => _songData.Value<int>("ART_ID");
	    public string ArtistName => _songData.Value<string>("ART_NAME");

	    public string SongTitle => _songData.Value<string>("SNG_TITLE");
	    public string Version => _songData.Value<string>("VERSION");

		public float Bpm => _songData.Value<float>("BPM");
		public int DiskNumber => _songData.Value<int>("DISK_NUMBER");
	    public int Duration => _songData.Value<int>("DURATION");
	    public float Gain => _songData.Value<float>("GAIN");
	    public int GenreId => _songData.Value<int>("GENRE_ID");
	    public DateTime ReleaseDateDigital => _songData.Value<DateTime>("DIGITAL_RELEASE_DATE");
	    public DateTime ReleaseDatePhysical => _songData.Value<DateTime>("PHYSICAL_RELEASE_DATE");
	    public int SongRank => _songData.Value<int>("RANK_SNG");
	    public int TrackNumber => _songData.Value<int>("TRACK_NUMBER");

	    internal string OriginMd5 => _songData.Value<string>("MD5_ORIGIN");
	    internal int MediaVersion => _songData.Value<int>("MEDIA_VERSION");

		private readonly JToken _songData;

	    internal DeezerSong(JToken data)
	    {
		    _songData = data;
	    }
    }
}
