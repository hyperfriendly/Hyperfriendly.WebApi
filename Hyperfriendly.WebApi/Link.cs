namespace Hyperfriendly.WebApi
{
    public class Link
    {
        public Link(string rel, string href, string method = "GET")
        {
            Rel = rel;
            Href = href;
            Method = method;
        }

        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
        public string Type { get; set; }
    }
}