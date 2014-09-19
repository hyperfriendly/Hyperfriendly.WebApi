using System.Web.Http;
using Owin;

namespace Hyperfriendly.WebApi.Example
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Formatters.Clear();
            config.Formatters.Add(new HyperfriendlyJsonMediaTypeFormatter());

            appBuilder.UseWebApi(config);
        }
    }
}