using System;
using System.Net;
using System.Text.RegularExpressions;
using DeezSharp.Helpers;

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
	}
}