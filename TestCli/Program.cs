using System;
using System.Collections.Generic;
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
	                IEnumerable<DeezerSongX> tracks = d.GetTracks(a.Tracks);
	                Console.WriteLine($"Will download with quality {Quality}.");

                    foreach (DeezerSongX s in tracks) {
	                    Console.WriteLine($"[{s.TrackNumber.ToString().PadLeft(a.AmountOfTracks.ToString().Length)}/" +
	                                      $"{a.AmountOfTracks}] Downloading '{s.ArtistName} - {s.SongTitle}'...");
	                    d.SaveTrack(s, a.AlbumName, Quality, a);
	                }
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
