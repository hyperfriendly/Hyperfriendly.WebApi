using System.Linq;
using Newtonsoft.Json.Linq;
using Should;
using Xunit;

namespace Hyperfriendly.WebApi.Tests
{
    public class formatting_hyperfriendly_json
    {
        private static readonly HyperfriendlyJsonMediaTypeFormatter _mediaFormatter = new HyperfriendlyJsonMediaTypeFormatter {Indent = true};

        [Fact]
        public void supports_hyperfriendly_json_media_type()
        {
            _mediaFormatter.SupportedMediaTypes.First().MediaType.ShouldEqual("vnd/hyperfriendly+json");
        }

        [Fact]
        public void links_element_is_json_object()
        {
            var resource = new FooResource();
            resource.Links.Add(new Link("self", "http://api.example.org/foo"));

            var json = _mediaFormatter.Format(resource);

            json.SelectToken("_links").Type.ShouldEqual(JTokenType.Object);
        }

        [Fact]
        public void resource_properties_are_in_camel_case()
        {
            var resource = new FooResource {Bar = "Baz"};

            var json = _mediaFormatter.Format(resource);

            json.Value<string>("bar").ShouldEqual("Baz");
        }

        [Fact]
        public void rel_with_single_link_is_json_object()
        {
            var resource = new FooResource();
            resource.Links.Add(new Link("self", "http://api.example.org/foo"));

            var json = _mediaFormatter.Format(resource);

            json.SelectToken("_links.self").Type.ShouldEqual(JTokenType.Object);
        }

        [Fact]
        public void rel_with_multiple_links_is_json_array()
        {
            var resource = new FooResource();
            resource.Links.Add(new Link("alternate", "http://api.example.org/baz"));
            resource.Links.Add(new Link("alternate", "http://api.example.org/bar"));

            var json = _mediaFormatter.Format(resource);

            json.SelectToken("_links.alternate").Type.ShouldEqual(JTokenType.Array);
        }
    }

    public class FooResource : Resource
    {
        public string Bar { get; set; }
    }
}