using System;
using DeezSharp;

namespace TestCli
{
	internal class Program
    {
	    private static void Main(string[] args)
        {
	        Console.WriteLine("DeezSharp test project\n");

	        Console.WriteLine("Creating Deezer object...");
	        var d = new Deezer();
	        Console.WriteLine("Initializing API...");
			d.Init();
        }
    }
}
