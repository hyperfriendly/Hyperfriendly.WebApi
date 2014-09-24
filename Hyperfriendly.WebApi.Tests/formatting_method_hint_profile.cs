using Newtonsoft.Json.Linq;
using Should;
using Xunit;

namespace Hyperfriendly.WebApi.Tests
{
    public class formatting_method_hint_profile
    {
        private static readonly HyperfriendlyJsonMediaTypeFormatter _mediaFormatter = new HyperfriendlyJsonMediaTypeFormatter { Indent = true };

        [Fact]
        public void a_link_with_a_method_hint_should_contain_method_hint_link_profile()
        {
            var resource = new Resource();
            resource.Links.Add(new Link("some_resource", "http://api.example.com/some_resource") { Method = "POST" });

            var jToken = _mediaFormatter.Format(resource);

            jToken.SelectToken("_links.some_resource.linkProfiles[0]").Value<string>().ShouldEqual("http://profiles.hyperfriendly.net/method-hint");
        }

        [Fact]
        public void a_link_with_a_method_hint_should_contain_method_property()
        {
            var resource = new Resource();
            resource.Links.Add(new Link("some_resource", "http://api.example.com/some_resource") { Method = "POST" });

            var jToken = _mediaFormatter.Format(resource);

            jToken.SelectToken("_links.some_resource.method").Value<string>().ShouldEqual("POST");
        }
    }
}