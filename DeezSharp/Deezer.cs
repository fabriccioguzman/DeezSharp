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

		public void SaveTrack(DeezerSongX s, string directory, SongQuality quality = SongQuality.MP3_320)
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

		    byte[] id3Data = new ID3v2Creator(s).GetAllBytes();
            
            byte[] data = id3Data.Concat(DeezerUtils.DecryptSongData(Web.DownloadData(DeezerUtils.GetDownloadUrl(s, quality)), s.SongId)).ToArray();
            
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			string path = Path.Combine(directory, $"{s.ArtistName} - {$"{s.SongTitle} {s.Version}".TrimEnd(' ')}.{ext}");
			File.WriteAllBytes(path, data);
	    }

	    public DeezerSongX GetTrack(int id)
	    {
	        var song = QueryTrack(id).First();
            Debug.Assert(song.SongId == id);
	        return song;
	    }

        public IEnumerable<DeezerSongX> GetTracks(int[] id)
        {
            return QueryTrack(id);
        }

	    public IEnumerable<DeezerSongX> GetTracks(IEnumerable<DeezerSong> lite)
	    {
	        return QueryTrack(lite.Select(a => a.SongId).ToArray());
	    }

	    public DeezerAlbum GetAlbum(int albumId)
	    {
	        DeezerAlbum album = QueryAlbum(albumId);
            Debug.Assert(album.AlbumId == albumId);
            return album;
	    }

		private IEnumerable<DeezerSongX> QueryTrack(params int[] id)
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

            //it is an absolute fucking pain to figure this out without StackOverflow
		    return response["results"]["data"].ToObject<IEnumerable<DeezerSongX>>();
		}

	    private DeezerAlbum QueryAlbum(int id)
	    {
	        string responseString = Web.DownloadString($"{Constants.UrlPublicApi}/album/{id}");
	        return JsonConvert.DeserializeObject<DeezerAlbum>(responseString);
	    }
	}
}