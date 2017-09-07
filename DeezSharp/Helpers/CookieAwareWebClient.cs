using System;
using System.Net;

namespace DeezSharp.Helpers
{
    internal class CookieAwareWebClient : WebClient
    {
	    public CookieContainer Cookies = new CookieContainer();
	    private string lastPage;

	    protected override WebRequest GetWebRequest(System.Uri address)
	    {
		    WebRequest req = base.GetWebRequest(address);
		    if (req is HttpWebRequest request) {
			    request.CookieContainer = Cookies;
			    if (lastPage != null)
				    request.Referer = lastPage;
		    }
		    lastPage = address.ToString();
		    req.Timeout = 1000;
		    return req;
	    }
    }
}
