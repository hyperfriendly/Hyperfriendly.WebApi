using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace Hyperfriendly.WebApi
{
    public class HyperfriendlyJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        readonly LinksConverter linksConverter = new LinksConverter();
        public HyperfriendlyJsonMediaTypeFormatter()
        {
            //SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hyperfriendly+json"));
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            SerializerSettings.Converters.Add(linksConverter);
        }
    }
}