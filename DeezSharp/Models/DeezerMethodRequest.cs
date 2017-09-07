using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DeezSharp.Models
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class DeezerMethodRequest
    {
	    public string method;
		public Dictionary<string, object> @params;
    }
}
