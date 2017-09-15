using System;
using System.Collections.Generic;
using System.Linq;
using DeezSharp;
using DeezSharp.Models;

namespace TestCli
{
	internal class Program
    {
	    private const int SampleSong = 372123951;
        private const int SampleAlbum = 15218775;
        private const string SearchQuery = "Da Tweekaz";
        private const SongQuality Quality = SongQuality.MP3_128;

        private const string Action = "search";

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
	            case "search": {
	                Console.WriteLine("Searching...");
	                DeezerSong[] tracks = d.SearchSongs(SearchQuery).ToArray(); //will be enumerated more than once

	                if (tracks.Length == 0) {
	                    Console.WriteLine("No results found.");
	                    break;
	                }

	                const string headerTrackId = "Trk. ID";
	                const string headerAlbumId = "Alb. ID";
	                const string headerArtistId = "Art. ID";
	                const string headerTrack = "Full Name";

                    int lenTrackId =  Math.Max(tracks.Max(a => a.SongId.ToString().Length), headerTrack.Length);
	                int lenAlbumId =  Math.Max(tracks.Max(a => a.Album.AlbumId.ToString().Length), headerAlbumId.Length);
	                int lenArtistId = Math.Max(tracks.Max(a => a.Artist.ArtistId.ToString().Length), headerArtistId.Length);
	                int lenTrack =    Math.Max(tracks.Max(a => $"{a.Artist.ArtistName} - {a.SongTitle}".ToString().Length), headerTrack.Length);

                    Console.WriteLine($"|{headerTrackId.PadRight(lenTrackId)}" +
                                      $"|{headerAlbumId.PadRight(lenAlbumId)}" +
                                      $"|{headerArtistId.PadRight(lenArtistId)}" +
                                      $"|{headerTrack.PadRight(lenTrack)}|");
	                Console.WriteLine($"|{new string('-', lenTrackId)}|{new string('-', lenAlbumId)}|{new string('-', lenArtistId)}|{new string('-', lenTrack)}|");
                    foreach (var t in tracks) {
	                    Console.WriteLine($"|{t.SongId.ToString().PadLeft(lenTrackId)}" +
	                                      $"|{t.Album.AlbumId.ToString().PadLeft(lenAlbumId)}" +
	                                      $"|{t.Artist.ArtistId.ToString().PadLeft(lenArtistId)}" +
	                                      $"|{$"{t.Artist.ArtistName} - {t.SongTitle}".PadRight(lenTrack)}|");
	                }
	                Console.WriteLine($"|{new string('-', lenTrackId)}|{new string('-', lenAlbumId)}|{new string('-', lenArtistId)}|{new string('-', lenTrack)}|");
                    break;
	            }
	        }
#if DEBUG
	        Console.WriteLine("Done!");
            Console.Read();
#endif
	    }
    }
}
