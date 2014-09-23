using System.Net.Http;
using System.Web.Http;

namespace Hyperfriendly.WebApi.Example
{
    public class FailingController : ApiController
    {
        public HttpResponseMessage Get()
        {
            throw new OhNoException("Dang!");
        }
    }
}