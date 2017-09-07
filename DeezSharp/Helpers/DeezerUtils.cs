using System;
using System.Text;

namespace DeezSharp.Helpers
{
    internal static class DeezerUtils
	{

		public static string GetDownloadUrl(DeezerSong s, SongQuality format) 
			=> GetDownloadUrl(s.OriginMd5, s.SongId, s.MediaVersion, (int)format);

		public static string GetDownloadUrl(string md5Origin, int id, int mediaVersion, int format)
	    {
			string str = $"{md5Origin}¤{format}¤{id}¤{mediaVersion}";
		    str = $"{CryptoHelper.HashMD5(str)}¤{str}¤";

		    string encrypted = CryptoHelper.EncryptAes128(str, "jo6aey6haid2Teih", string.Empty);

		    return $"http://e-cdn-proxy-{md5Origin[0]}.deezer.com/mobile/1/{encrypted}";
	    }

	    public static byte[] DecryptSongData(byte[] data, int songId)
	    {
		    const int intervalChunk = 3;
		    const int chunkSize = 2048;

		    byte[] blowfishKey = GetBlowfishKey(Math.Abs(songId));
		    byte[] blowfishIV = {0, 1, 2, 3, 4, 5, 6, 7};	//very original, guys
			
		    int index = 0;
		    int position = 0;

		    byte[] ret = new byte[data.Length + (chunkSize - (data.Length - data.Length / chunkSize * chunkSize))];    //practically Ceil(len/chunk)*chunk

			while (position <= data.Length) {
			    byte[] chunk = new byte[chunkSize];
				Array.Copy(data, position, chunk, 0, Math.Min(chunk.Length, data.Length - position));
			    if (index % intervalChunk == 0) {
				    chunk = CryptoHelper.DecryptBlowfishCbc(chunk, blowfishKey, blowfishIV);
			    }
				chunk.CopyTo(ret, position);
			    position += chunkSize;
			    index++;
		    }
		    return ret;
	    }

	    private static byte[] GetBlowfishKey(int songId)
	    {
		    Encoding enc = Encoding.GetEncoding("ISO-8859-1");	//latin1
		    const string seed = "g4el58wc0zvf9na1";

			string hash = CryptoHelper.HashMD5(songId.ToString());

		    byte[] p0 = enc.GetBytes(seed);
			byte[] p1 = enc.GetBytes(hash.Substring(0, 16));
		    byte[] p2 = enc.GetBytes(hash.Substring(16, 16));

		    return CryptoHelper.Xor(p0, p1, p2);
	    }
    }
}
