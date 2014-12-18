using System.Net;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Hyperfriendly.WebApi.ErrorHandling;
using Owin;

namespace Hyperfriendly.WebApi.Example
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { controller = "Home", id = RouteParameter.Optional }
                );

            config.Formatters.Clear();
            config.Formatters.Add(new HyperfriendlyJsonMediaTypeFormatter());
            var transformer = new ExceptionTransformer{ DefaultTransformer = e => new ErrorResponse(HttpStatusCode.InternalServerError, "Error!", "Something bad happened!")};
            transformer.AddTransformer<OhNoException>(e => new ErrorResponse(HttpStatusCode.BadRequest, e.Shout, e.Shout + " Something bad happened!"));
            config.Services.Replace(typeof(IExceptionHandler), new HyperfriendlyExceptionHandler(transformer));
            appBuilder.UseWebApi(config);
        }
    }
}

