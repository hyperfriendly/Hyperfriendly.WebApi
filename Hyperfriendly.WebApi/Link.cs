using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;

namespace Hyperfriendly.WebApi
{
    public class Link
    {
        public Link(string rel, string relativePath)
        {
            Rel = rel;
            _relativePath = relativePath;
        }

        public string Rel { get; private set; }
        private readonly string _relativePath;
        public string Method { get; set; }

        public HttpRequestMessage Request { private get; set; }

        public string Href
        {
            get
            {
                if (_relativePath.IndexOf("http", StringComparison.InvariantCultureIgnoreCase) >= 0 || Request == null)
                {
                    return _relativePath;
                }

                return string.Format("{0}://{1}{2}/{3}", Request.RequestUri.Scheme, Request.RequestUri.Authority,
                HttpRuntime.AppDomainAppVirtualPath, _relativePath);
            }
        }

        public IEnumerable<string> LinkProfiles
        {
            get
            {
                if (Method != null)
                    yield return "http://profiles.hyperfriendly.net/method-hint";
            }
        }
    }
}