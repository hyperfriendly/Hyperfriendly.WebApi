using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;

namespace Hyperfriendly.WebApi.Tests
{
    static internal class MediaTypeFormatterExtensions
    {
        public static JToken Format(this MediaTypeFormatter formatter, Resource resource)
        {
            var content = new StringContent(String.Empty);
            var type = resource.GetType();

            using (var stream = new MemoryStream())
            {
                formatter.WriteToStreamAsync(type, resource, stream, content, null);
                stream.Seek(0, SeekOrigin.Begin);
                var serialisedResult = new StreamReader(stream).ReadToEnd();
                return JToken.Parse(serialisedResult);
            }
        }
    }
}