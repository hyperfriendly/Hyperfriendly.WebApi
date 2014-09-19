using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hyperfriendly.WebApi
{
    public class HyperfriendlyJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        readonly LinksConverter _linksConverter = new LinksConverter();
        public HyperfriendlyJsonMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("vnd/hyperfriendly+json"));
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.Converters.Add(_linksConverter);
        }
    }
}