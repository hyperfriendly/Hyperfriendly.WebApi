using System.Net.Http;
using System.Web.Http;

namespace Hyperfriendly.WebApi.Example
{
    public class HomeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var resource = new Resource();
            resource.Links.Add(new Link("self", "/"));
            return Request.CreateResponse(resource);
        }
    }
}