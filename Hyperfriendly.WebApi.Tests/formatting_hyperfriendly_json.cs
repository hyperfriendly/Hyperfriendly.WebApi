using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Should;
using Xunit;

namespace Hyperfriendly.WebApi.Tests
{
    public class formatting_hyperfriendly_json
    {
        private static readonly HyperfriendlyJsonMediaTypeFormatter _mediaFormatter = new HyperfriendlyJsonMediaTypeFormatter {Indent = true};

        [Fact]
        public void formats_resource_properties_as_camel_case()
        {
            var resource = new FooResource {Bar = "Baz"};

            var json = Format(resource);

            json.Value<string>("bar").ShouldEqual("Baz");
        }

        [Fact]
        public void converts_links_to_json_object()
        {
            var resource = new FooResource();
            resource.Links.Add(new Link("self", "http://api.example.org/foo"));
            
            var json = Format(resource);

            json.SelectToken("_links").Type.ShouldEqual(JTokenType.Object);
        }

        [Fact]
        public void converts_rel_with_single_links_to_json_object()
        {
            var resource = new FooResource();
            resource.Links.Add(new Link("self", "http://api.example.org/foo"));

            var json = Format(resource);

            json.SelectToken("_links.self").Type.ShouldEqual(JTokenType.Object);
        }

        [Fact]
        public void converts_rel_with_multiple_links_to_json_array()
        {
            var resource = new FooResource();
            resource.Links.Add(new Link("alternate", "http://api.example.org/baz"));
            resource.Links.Add(new Link("alternate", "http://api.example.org/bar"));

            var json = Format(resource);

            json.SelectToken("_links.alternate").Type.ShouldEqual(JTokenType.Array);
        }

        [Fact]
        public void supports_hyperfriendly_json_media_type()
        {
            _mediaFormatter.SupportedMediaTypes.First().MediaType.ShouldEqual("vnd/hyperfriendly+json");
        }

        private static JToken Format(FooResource resource)
        {
            var content = new StringContent(string.Empty);
            var type = resource.GetType();

            using (var stream = new MemoryStream())
            {
                _mediaFormatter.WriteToStreamAsync(type, resource, stream, content, null);
                stream.Seek(0, SeekOrigin.Begin);
                var serialisedResult = new StreamReader(stream).ReadToEnd();
                return JToken.Parse(serialisedResult);
            }
        }
    }

    public class FooResource : Resource
    {
        public string Bar { get; set; }
    }
}