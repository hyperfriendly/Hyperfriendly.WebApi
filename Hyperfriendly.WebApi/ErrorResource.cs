using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hyperfriendly.WebApi
{
    public class ErrorResource : Resource
    {
        public ErrorResource()
        {
            Links.Add(new Link("profile", Profiles.Error));
            Errors = new List<ErrorEntryResource>();
        }

        [JsonProperty(PropertyName = "_errors")]
        public IList<ErrorEntryResource> Errors { get; set; }
    }
}