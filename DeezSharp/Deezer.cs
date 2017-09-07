using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using DeezSharp.Helpers;
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

			byte[] data = DeezerUtils.DecryptSongData(Web.DownloadData(DeezerUtils.GetDownloadUrl(s, quality)), s.SongId);

			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			string path = Path.Combine(directory, $"{s.ArtistName} - {s.SongTitle}.{ext}");
			File.WriteAllBytes(path, data);
		}

		public DeezerSong GetTrack(int id)
		{
			JToken song = QueryTrack(id);
			Debug.Assert((int)song["SNG_ID"] == id);

			return new DeezerSong(song);
		}

		private JToken QueryTrack(int id)
		{
			var query = new DeezerMethodRequest
			{
				method = "song.getListData",
				@params = new Dictionary<string, object> { { "sng_ids", new[] { id } } }
			};
			string queryString = JsonConvert.SerializeObject(new[] { query });

			string responseString = Web.UploadString($"{Constants.UrlApi}?api_version=1.0&api_token={_token}&input=3", queryString);
			JToken response = ((JArray)JsonConvert.DeserializeObject(responseString))[0];

			if (response["error"].HasValues)
				throw new Exception("Deezer reported back an error. " + response["error"].First);

			JToken results = response["results"]["data"][0];    //only support single queries for now
			return results;
		}
	}
}