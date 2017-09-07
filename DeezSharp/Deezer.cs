using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		public void GetTrack(int id)
		{
			JToken song = QueryTrack(id);
			Debug.Assert((int)song["SNG_ID"] == id);

			string origin = (string)song["MD5_ORIGIN"];
			int mVer = (int)song["MEDIA_VERSION"];

			var url = DeezerUtils.GetDownloadUrl(origin, id, 3, mVer);
		}

		private JToken QueryTrack(int id)
		{
			var query = new DeezerMethodRequest
			{
				method = "song.getListData",
				@params = new Dictionary<string, object> { { "sng_ids", new[] { id } } }
			};
			var queryString = JsonConvert.SerializeObject(new[] { query });

			string responseString = Web.UploadString($"{Constants.UrlApi}?api_version=1.0&api_token={_token}&input=3", queryString);
			JToken response = ((JArray)JsonConvert.DeserializeObject(responseString))[0];

			if (response["error"].HasValues)
				throw new Exception("Deezer reported back an error. " + response["error"].First);

			JToken results = response["results"]["data"][0];    //only support single queries for now
			return results;
		}
	}
}