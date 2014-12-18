using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hyperfriendly.WebApi.Example
{
    [Route("RouteAttr")]
    public class RouteAttributeControllers: ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    [Route("RouteAttrWithParam/{p1}/{p2}")]
    public class RouteAttributeWithParamController: ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    [Route("RouteAttrWithOptionalParam/{p1?}")]
    public class RouteAttributeWithOptionalParamController: ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    [Route("RouteAttrWithConditionalParam/{p1:int}")]
    public class RouteAttributeWithCondirtionalParamController: ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }

    [Route("RouteAttrWithQueryParam")]
    [QueryParametersAttribute("p1", "p2")]
    public class RouteAttributeWithQueryParamController : ApiController
    {
        public HttpResponseMessage Get([FromUri] string p1)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new{ p1});
        }
    }

    [Route("RouteAttrWithEVERYTHING/{p1}")]
    [QueryParametersAttribute("p1", "p2")]
    public class RouteAttributeWithEverythingController : ApiController
    {
        public HttpResponseMessage Get([FromUri] string p1)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new{ p1});
        }
    }



}
