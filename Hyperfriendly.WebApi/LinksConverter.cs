using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Hyperfriendly.WebApi
{
    public class LinksConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var links = (IList<Link>)value;
            var lookup = links.ToLookup(l => l.Rel);
            if (lookup.Count == 0) return;

            writer.WriteStartObject();

            foreach (var rel in lookup)
            {
                writer.WritePropertyName(rel.Key);
                foreach (var link in rel)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("href");
                    writer.WriteValue(link.Href);

                    writer.WriteEndObject();
                }
            }
            writer.WriteEndObject();
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IList<Link>).IsAssignableFrom(objectType);
        }
    }
}