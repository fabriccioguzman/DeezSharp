using System;
using DeezSharp;
using DeezSharp.Models;

namespace TestCli
{
	internal class Program
    {
	    private const int SampleSong = 372123951;
        private const int SampleAlbum = 15218775;
        private const SongQuality Quality = SongQuality.MP3_128;

        private const string Action = "album";

	    private static void Main(string[] args)
	    {
	        Console.WriteLine("DeezSharp test project\n");

	        var d = new Deezer();
	        Console.WriteLine("Initializing API...");
			d.Init();

	        switch (Action) {
	            case "song": {
	                Console.WriteLine("Getting track info...");
	                DeezerSongX s = d.GetTrack(SampleSong);
	                Console.WriteLine($"Downloading {Quality} track...");
	                d.SaveTrack(s, ".", Quality);
	                break;
	            }
	            case "album": {
	                Console.WriteLine("Getting info from album...");
	                DeezerAlbum a = d.GetAlbum(SampleAlbum);
	                Console.WriteLine($"Album '{a.AlbumName}' has {a.AmountOfTracks} tracks, totalling {new TimeSpan(0, 0, a.TotalLength)} of listening joy!");
	                Console.WriteLine("Getting full track info...");
	                foreach (DeezerSongX s in d.GetTracks(a.Tracks)) {
	                    Console.WriteLine($"[{Quality}] Downloading '{s.ArtistName} - {s.SongTitle}'...");
	                    d.SaveTrack(s, a.AlbumName, Quality);
	                }
                    /*
                    foreach (DeezerSongX song in d.GetAlbumTracks(SampleAlbum)) {
	                    Console.Write($"[{song.TrackNumber.ToString().PadLeft(2)}] Downloading track \"{song.SongTitle}\" at quality {Quality}...");
                        d.SaveTrack(song, "album", Quality);
	                }*/
                    break;
	            }
	        }
	        Console.WriteLine("Done!");

#if DEBUG
	        Console.Read();
#endif
	    }
    }
}
