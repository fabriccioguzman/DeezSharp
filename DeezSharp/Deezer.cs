using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using DeezSharp.Helpers;
using DeezSharp.Models;
using Newtonsoft.Json;

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

		public void GetTrack(int id)
		{
			var query = new DeezerMethodRequest {
				method = "song.getListData",
				@params = new Dictionary<string, object> { {"sng_ids", new[] {id} } }
			};
			var queryString = JsonConvert.SerializeObject(new[] {query});

			string response = Web.UploadString($"{Constants.UrlApi}?api_version=1.0&api_token={_token}&input=3", queryString);
		}
	}
}