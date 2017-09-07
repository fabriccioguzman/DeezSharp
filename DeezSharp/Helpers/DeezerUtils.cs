using System;
using System.Collections.Generic;
using System.Text;

namespace DeezSharp.Helpers
{
    internal static class DeezerUtils
    {
	    public static string GetDownloadUrl(string md5Origin, int id, int format, int mediaVersion)
	    {
			string str = $"{md5Origin}¤{format}¤{id}¤{mediaVersion}";
		    str = $"{CryptoHelper.HashMD5(str)}¤{str}¤";

		    string encrypted = CryptoHelper.EncryptAes128(str, "jo6aey6haid2Teih", string.Empty);

		    return $"http://e-cdn-proxy-{md5Origin[0]}.deezer.com/mobile/1/{encrypted}";
	    }
    }
}
