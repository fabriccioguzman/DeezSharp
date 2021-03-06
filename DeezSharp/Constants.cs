﻿using System;

namespace DeezSharp
{
    internal class Constants
    {
	    public const string UrlBase = "http://www.deezer.com";
		public const string UrlApi = UrlBase + "/ajax/gw-light.php";
	    public const string UrlCovers = "http://e-cdn-images.deezer.com/images/cover/";
        public const string UrlPublicApi = "http://api.deezer.com";

		public const string HeaderUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";

	    public const string RegexToken = "checkForm = \"(.+)\"";
    }
}
