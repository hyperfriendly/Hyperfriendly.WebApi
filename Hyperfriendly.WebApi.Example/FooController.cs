using System;
using System.Net;
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
            resource.Links.Add(new Link("failing", "/Failing"));
            return Request.CreateResponse(resource);
        }

        public HttpResponseMessage Post()
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.Created);
            var link = Url.Link("DefaultApi", new {controller = "Foo"});
            responseMessage.Headers.Location = new Uri(link);
            return responseMessage;
        }
    }
}