using System;
using System.Net;

namespace SportsScheduler.API.Helper
{
    public class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        public CookieAwareWebClient()
        {
            
        }

        public CookieAwareWebClient(CookieContainer cookieContainer)
        {
            _cookieContainer = cookieContainer;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            var webRequest = request as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.CookieContainer = _cookieContainer;
            }
            return request;
        }
    }
}
