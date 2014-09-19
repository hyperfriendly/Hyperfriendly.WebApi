using System.Net.Http;
using System.Web.Http;
using Hyperfriendly.WebApi.Example.Resources;

namespace Hyperfriendly.WebApi.Example
{
    public class FooController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var resource = new FooResource();
            resource.Links.Add(new Link("self", "/Foo"));
            resource.Links.Add(new Link("bars", "/BarCollection"));
            return Request.CreateResponse(resource);
        }
    }
}