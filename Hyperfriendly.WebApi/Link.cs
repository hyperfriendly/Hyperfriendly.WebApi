using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Hyperfriendly.WebApi
{
    public class Link
    {
        public Link(string rel, string href)
        {
            Rel = rel;
            Href = href;
        }

        public string Rel { get; private set; }
        public string Href { get; private set; }
        public string Method { get; set; }

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