using Newtonsoft.Json.Linq;
using Should;
using Xunit;

namespace Hyperfriendly.WebApi.Tests
{
    public class formatting_error_profile
    {
        private static readonly HyperfriendlyJsonMediaTypeFormatter _mediaFormatter = new HyperfriendlyJsonMediaTypeFormatter { Indent = true };

        [Fact]
        public void contains_profile()
        {
            var errorResource = new ErrorResource();

            var jToken = _mediaFormatter.Format(errorResource);

            jToken.SelectToken("_links.profile.href").Value<string>().ShouldEqual(Profiles.Error);
        }    

        [Fact]
        public void contains_errors_array()
        {
            var errorResource = new ErrorResource();

            var jToken = _mediaFormatter.Format(errorResource);

            jToken.SelectToken("_errors").Type.ShouldEqual(JTokenType.Array);
        }

        [Fact]
        public void error_items_are_formatted_correctly()
        {
            var errorResource = new ErrorResource();
            errorResource.Errors.Add(new ErrorEntryResource("a title", "a message"));
            var jToken = _mediaFormatter.Format(errorResource);

            jToken.SelectToken("_errors[0].title").Value<string>().ShouldEqual("a title");
            jToken.SelectToken("_errors[0].message").Value<string>().ShouldEqual("a message");
        }
    }
}