using Newtonsoft.Json.Linq;
using Should;
using Xunit;

namespace Hyperfriendly.WebApi.Tests
{
    public class formatting_collection_profile
    {
        private static readonly HyperfriendlyJsonMediaTypeFormatter _mediaFormatter = new HyperfriendlyJsonMediaTypeFormatter { Indent = true };

        [Fact]
        public void contains_profile()
        {
            var collectionResource = new CollectionResource<FooResource>();

            var jToken = _mediaFormatter.Format(collectionResource);

            jToken.SelectToken("_links.profile.href").Value<string>().ShouldEqual(Profiles.Collection);
        }

        [Fact]
        public void items_element_is_json_array()
        {
            var collectionResource = new CollectionResource<FooResource>();

            var jToken = _mediaFormatter.Format(collectionResource);

            jToken
                .SelectToken("_items")
                .Type
                .ShouldEqual(JTokenType.Array);
        }

        [Fact]
        public void items_can_contain_resources()
        {
            var collectionResource = new CollectionResource<FooResource>();
            var resource = new FooResource { Bar = "Foo" };
            resource.Links.Add(new Link("self", "/foo"));
            collectionResource.Items.Add(resource);

            var jToken = _mediaFormatter.Format(collectionResource);

            jToken
                .SelectToken("_items[0].bar")
                .Value<string>()
                .ShouldEqual("Foo");
        }
    }
}