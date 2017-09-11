using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DeezSharp.Models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class DeezerMethodRequest
    {
	    public string method;
		public Dictionary<string, object> @params;
    }
}
