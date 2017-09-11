using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using DeezSharp.Helpers;
using DeezSharp.Metadata;
using DeezSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeezSharp
{
	public class Deezer
	{
		public WebClient Web = new CookieAwareWebClient();

		private string _token;

		public void Init()
		{
			string src = Web.DownloadString(Constants.UrlBase);

			var m = Regex.Match(src, Constants.RegexToken);
			if (!m.Success || m.Groups.Count != 2)
				throw new Exception("Could not find API token.");
			_token = m.Groups[1].Value;
		}

		public void SaveTrack(DeezerSong s, string directory, SongQuality quality = SongQuality.MP3_320)
		{
			string ext;
			switch (quality) {
				case SongQuality.M4A_96:
					ext = "m4a";
					break;
				case SongQuality.FLAC:
					ext = "flac";
					break;
				case SongQuality.MP3_128:
				case SongQuality.MP3_320:
				default:
					ext = "mp3";
					break;
			}

		    byte[] id3v2 = new ID3v2Creator(s).GetAllBytes();
            
            byte[] data = id3v2.Concat(DeezerUtils.DecryptSongData(Web.DownloadData(DeezerUtils.GetDownloadUrl(s, quality)), s.SongId)).ToArray();
            
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			string path = Path.Combine(directory, $"{s.ArtistName} - {$"{s.SongTitle} {s.Version}".TrimEnd(' ')}.{ext}");
			File.WriteAllBytes(path, data);
	    }

	    public DeezerSong GetTrack(int id)
	    {
	        JToken token = QueryTrack(id).First();
            Debug.Assert((int)token["SNG_ID"] == id);
	        return new DeezerSong(token);
	    }

        public IEnumerable<DeezerSong> GetTracks(int[] id)
        {
            return QueryTrack(id).Select(token => new DeezerSong(token));
        }

	    public IEnumerable<DeezerSong> GetTracks(IEnumerable<DeezerSongLite> lite)
	    {
	        return QueryTrack(lite.Select(a => a.SongId).ToArray()).Select(token => new DeezerSong(token));
	    }

        //TODO: rename to GetAlbum when DeezerSongLite is implemented
	    public DeezerAlbum GetAlbumInfo(int albumId)
	    {
	        JToken token = QueryAlbum(albumId);
            Debug.Assert((int)token["id"] == albumId);
            return new DeezerAlbum(token);
	    }

		private IEnumerable<JToken> QueryTrack(params int[] id)
		{
			var query = new DeezerMethodRequest
			{
				method = "song.getListData",
				@params = new Dictionary<string, object> { { "sng_ids", id } }
			};
			string queryString = JsonConvert.SerializeObject(new[] { query });

			string responseString = Web.UploadString($"{Constants.UrlApi}?api_version=1.0&api_token={_token}&input=3", queryString);
			JToken response = JsonConvert.DeserializeObject<JArray>(responseString)[0];

			if (response["error"].HasValues)
				throw new Exception("Deezer reported back an error. " + response["error"].First);

		    return response["results"]["data"].Children();
		}

	    private JToken QueryAlbum(int id)
	    {
	        string responseString = Web.DownloadString($"{Constants.UrlPublicApi}/album/{id}");
	        return JsonConvert.DeserializeObject<JToken>(responseString);
	    }
	}
}