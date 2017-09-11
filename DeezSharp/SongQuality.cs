using System;
using System.Diagnostics.CodeAnalysis;

namespace DeezSharp
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum SongQuality
    {
		MP3_128 = 0,    //also seems to be 1, investigate?
		MP3_320 = 3,
		M4A_96 = 8,
		FLAC = 9
    }
}
