using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hyperfriendly.WebApi
{
    public class CollectionResource<TResource> : Resource where TResource : Resource
    {
        public CollectionResource()
        {
            Items = new List<TResource>();
            Links.Add(new Link("profile", Profiles.Collection));
        }

        [JsonProperty(PropertyName = "_items")]
        public IList<TResource> Items { get; private set; }
    }
}