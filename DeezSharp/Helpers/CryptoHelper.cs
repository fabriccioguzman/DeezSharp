using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DeezSharp.Helpers
{
    internal static class CryptoHelper
    {
		private static readonly Encoding Latin1Encoding = Encoding.GetEncoding("ISO-8859-1");
	    private static readonly Encoding UTF8Encoding = Encoding.UTF8;


		public static string HashMD5(string input)  => string.Join(string.Empty, HashMD5Raw(input).Select(a => a.ToString("x2")));
	    public static byte[] HashMD5Raw(string input) => HashMD5Raw(Latin1Encoding.GetBytes(input));

		public static byte[] HashMD5Raw(byte[] input) => MD5.Create().ComputeHash(input);

	    public static string EncryptAes128(string input, string key, string iv) 
			=> string.Join(string.Empty, EncryptAes128(
				Latin1Encoding.GetBytes(input),
				Latin1Encoding.GetBytes(key),
				Latin1Encoding.GetBytes(iv)).Select(a => a.ToString("x2")));

	    public static byte[] EncryptAes128(byte[] input, byte[] key, byte[] iv)
	    {
		    var aes = Aes.Create();
		    aes.BlockSize = 128;
		    aes.Mode = CipherMode.ECB;

			//fix iv if needed
			if (iv == null || iv.Length == 0)
				iv = new byte[16];

		    using (var encr = aes.CreateEncryptor(key, iv)) {
				byte[] buffer = new byte[input.Length];
			    int l = encr.TransformBlock(input, 0, input.Length, buffer, 0);
				Debug.Assert(l == buffer.Length);
			    return buffer;
		    }
	    }
    }
}
