using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using DeezSharp.Helpers;

namespace DeezSharp.Metadata
{
	internal class ID3v2Creator
	{
        //see: http://id3.org/id3v2.3.0

        public Dictionary<string, FrameBase> Frames = new Dictionary<string, FrameBase>();
        
        //because lazy
	    public ID3v2Creator(DeezerSong s)
	    {
            //basic metadata
	        Frames["TIT2"] = new FrameString($"{s.SongTitle} {s.Version}".TrimEnd(' '));
	        Frames["TPE1"] = new FrameString(s.ArtistName);
            Frames["TALB"] = new FrameString(s.AlbumTitle);

            //original artist/song name
	        Frames["TOAL"] = new FrameString(s.SongTitle);
	        Frames["TOPE"] = new FrameString(s.ArtistName);

            //others
            Frames["TBPM"] = new FrameStringSimple(((int)s.Bpm).ToString());
	        Frames["TRCK"] = new FrameStringSimple(s.TrackNumber.ToString());

            //release date
	        Frames["TRDA"] = new FrameStringSimple($"{s.ReleaseDatePhysical}");
            Frames["TDAT"] = new FrameStringSimple($"{s.ReleaseDatePhysical.Day:00}{s.ReleaseDatePhysical.Month:00}");
	        Frames["TYER"] = new FrameStringSimple($"{s.ReleaseDatePhysical.Year:0000}");

            //TODO: TCON
            //TODO: TPE4
            //TODO: W* (eg. WOAF)
            //TODO: album art (A*)
            //TODO: POPM using log(rank)/log(totalrank)? :)
        }

        public byte[] GetAllBytes()
	    {
	        using (var ms = new MemoryStream())
	        using (var w = new BinaryWriterBigEndian(ms)) {
	            w.Write(Encoding.ASCII.GetBytes("ID3"));    //magic
                w.Write(new byte[] {0x03, 0x00});           //ID3 v2.3.0
	            w.Write((byte) 0b0000_0000);                //flags
                w.Write(Get7BitEncodedInt(Frames.Sum(a => 
                (a.Value.GetLength() ?? a.Value.GetBytes().Length) + 10)));  //total length, encoded as a 28bit int (hi bit 0)

	            foreach (var frame in Frames) {
	                if (frame.Key.Length != 4)
                        throw new Exception($"Invalid frame type {frame.Key}");

	                byte[] bytes = frame.Value.GetBytes();
                    if (bytes.Length < 1)
                        throw new Exception($"Frame {frame.Key} has no bytes!");

                    w.Write(Encoding.ASCII.GetBytes(frame.Key));        //4char key
                    w.Write(frame.Value.GetLength() ?? bytes.Length);   //32bit length
                    w.Write(frame.Value.GetFlags());                    //16bit flags
                    w.Write(frame.Value.GetBytes());                    //actual data
	            }

	            return ms.ToArray();
	        }
	    }

	    private static int Get7BitEncodedInt(int input)
	    {
	        //make sure input is not too big
            if ((input & 0x0FFFFFFF) != input)
                throw new Exception("ID3 data is too long!");

            //loop through bits in int and add to new int
	        int ret = 0;
	        ret += ((input & 0b00000000_00000000_00000000_01111111) >> 7 * 0) << 8 * 0;
	        ret += ((input & 0b00000000_00000000_00111111_10000000) >> 7 * 1) << 8 * 1;
	        ret += ((input & 0b00000000_00011111_11000000_00000000) >> 7 * 2) << 8 * 2;
	        ret += ((input & 0b00001111_11100000_00000000_00000000) >> 7 * 3) << 8 * 3;
	        return ret;

            //an actual loop, to make shaddy happy :)
            //the hardcoded 4 can be changed to BitConverter.GetBytes(default(4)).Length for optimal... something
	        //for (int i = 0; i < 4; i++)
	        //    ret += ((input & 0b01111111 << 7 * i) >> 7 * i) << 8 * i;
	    }
        
	    public abstract class FrameBase
	    {
	        public abstract byte[] GetBytes();
	        public virtual int? GetLength() => null;
            public virtual byte[] GetFlags() => new byte[] {0, 0};
	    }

	    public class FrameString : FrameBase
	    {
	        private string _str;
            public FrameString(string str)
            {
                _str = str;
            }

            public override byte[] GetBytes()
            {
                //we always use unicode for normal strings
                return (new byte[] {0x01})                  //encoding (unicode)
                    .Concat(new byte[] {0xFF, 0xFE})        //unicode byte order
                    .Concat(Encoding.Unicode.GetBytes(_str))//actual string
                    .Concat(new byte[] {0, 0}).ToArray();   //unicode null (string end)
            }
	    }

	    public class FrameStringSimple : FrameBase
	    {
	        private string _str;
	        public FrameStringSimple(string str)
	        {
	            _str = str;
	        }

	        public override byte[] GetBytes()
	        {
	            //we always use unicode for normal strings
	            return (new byte[] { 0x00 })                //encoding (ASCII/ISO-8859-1)
                    .Concat(Encoding.ASCII.GetBytes(_str))  //actual string
	                .Concat(new byte[] { 0 }).ToArray();    //null byte
	        }
	    }
    }
}