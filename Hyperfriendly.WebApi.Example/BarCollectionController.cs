using System.Net.Http;
using System.Web.Http;
using Hyperfriendly.WebApi.Example.Resources;

namespace Hyperfriendly.WebApi.Example
{
    public class BarCollectionController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var collectionResource = new CollectionResource<BarResource>();
            collectionResource.Links.Add(new Link("self", "/BarCollection"));
            var resource = new BarResource();
            resource.Links.Add(new Link("self", "/Bar/1"));
            collectionResource.Items.Add(resource);
            return Request.CreateResponse(collectionResource);
        }
    }
}