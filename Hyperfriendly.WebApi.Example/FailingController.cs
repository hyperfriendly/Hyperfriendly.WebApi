using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hyperfriendly.WebApi.Example
{
    public class FailingController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var errorResource = new ErrorResource();
            errorResource.Errors.Add(new ErrorEntryResource("Bad input!", "Something failed because of bad input!"));
            return Request.CreateResponse(HttpStatusCode.BadRequest, errorResource);
        }
    }
}