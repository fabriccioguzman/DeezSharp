using System;
using DeezSharp;

namespace TestCli
{
	internal class Program
    {
	    private const string SampleUrl = "http://www.deezer.com/track/372123951";
	    private const int SampleId = 372123951;
        private const SongQuality Quality = SongQuality.MP3_128;

        private const string Action = "song";

	    private static void Main(string[] args)
	    {
	        Console.WriteLine("DeezSharp test project\n");

	        var d = new Deezer();
	        Console.WriteLine("Initializing API...");
			d.Init();

	        switch (Action) {
	            case "song": {
	                Console.WriteLine("Getting track info...");
	                DeezerSong s = d.GetTrack(SampleId);
	                Console.WriteLine($"Downloading {Quality} track...");
	                d.SaveTrack(s, ".", Quality);
	                break;
	            }
	        }
        }
    }
}
