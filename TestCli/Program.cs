using System;
using DeezSharp;

namespace TestCli
{
	internal class Program
    {
	    private const string SampleUrl = "http://www.deezer.com/track/372123951";
	    private const int SampleId = 372123951;

	    private static void Main(string[] args)
	    {
	        Console.WriteLine("DeezSharp test project\n");

	        Console.WriteLine("Creating Deezer object...");
	        var d = new Deezer();
	        Console.WriteLine("Initializing API...");
			d.Init();
	        Console.WriteLine("Getting track...");
			d.GetTrack(SampleId);
        }
    }
}
