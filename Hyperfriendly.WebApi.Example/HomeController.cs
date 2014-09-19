using System.Net.Http;
using System.Web.Http;

namespace Hyperfriendly.WebApi.Example
{
    public class HomeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var resource = new Resource();
            resource.Links.Add(new Link("self", "/Home"));
            resource.Links.Add(new Link("foo", "/Foo"));
            return Request.CreateResponse(resource);
        }
    }
}