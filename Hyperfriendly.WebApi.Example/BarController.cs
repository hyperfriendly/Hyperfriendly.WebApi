using System.Net.Http;
using System.Web.Http;
using Hyperfriendly.WebApi.Example.Resources;

namespace Hyperfriendly.WebApi.Example
{
    public class BarController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            var resource = new BarResource();
            resource.Links.Add(new Link("self", "/Bar/1"));
            return Request.CreateResponse(resource);
        }
    }
}