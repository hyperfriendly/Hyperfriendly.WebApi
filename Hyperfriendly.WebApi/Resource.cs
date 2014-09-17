using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hyperfriendly.WebApi
{
    public class Resource
    {
        public Resource()
        {
            Links = new List<Link>();
        }

        [JsonProperty(PropertyName = "_links")]
        public List<Link> Links { get; set; }
    }
}