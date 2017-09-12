using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

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

	        using (var encr = aes.CreateEncryptor(key, iv))
	        using (var ms = new MemoryStream())
	        using (var cs = new CryptoStream(ms, encr, CryptoStreamMode.Write)) {
                cs.Write(input, 0, input.Length);
	            return ms.ToArray();
	        }
	    }

		public static byte[] DecryptBlowfishCbc(byte[] input, byte[] key, byte[] iv)
		{
			var cipher = new BufferedBlockCipher(new CbcBlockCipher(new BlowfishEngine()));
			cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));
			return cipher.ProcessBytes(input);
		}

		public static byte[] Xor(byte[] source, params byte[][] extra)
	    {
			byte[] ret = source;

		    for (int i = 0; i < ret.Length; i++)
			    foreach (byte[] e in extra)
				    ret[i] ^= e[i];

		    return ret;
	    }
    }
}
