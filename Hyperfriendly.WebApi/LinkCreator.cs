using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Hyperfriendly.WebApi
{
    public class LinkCreator
    {
        private bool _useNamedRoute;
        private string _routeName;
        private string _httpVerb;
        private string _rel;
        private Dictionary<string, object> _values;
        private HttpRequestMessage _request;

        private LinkCreator(string rel)
        {
            _useNamedRoute = false;
            _httpVerb = "GET";
            _routeName = "DefaultApi";
            _rel = rel;
            _values = new Dictionary<string, object>();
        }

        public static LinkCreator Create(string rel)
        {
            return new LinkCreator(rel);
        }

        public Link GetLink<TController>()
        {
            string href;

            var queryParams = GetQueryParams(typeof(TController));

            if (_useNamedRoute)
            {
                _values["controller"] = typeof (TController).Name.Replace("Controller", "");
                href = new UrlHelper(_request).Link(_routeName, _values);
            }
            else
            {
                var template = GetRouteTemplate(typeof(TController)).Template;
                href = Regex.Replace(template, @"\{([^\:]*?)(\:.*?|)(\?|)\}", match =>
                {
                    var value = match.Groups[1].Value;
                    return _values.ContainsKey(value)
                        ? _values[value].ToString()
                        : string.Format("{{{0}}}", value);
                });
            }

            href += GetQueryString(queryParams);

            return new Link(_rel, href);
        }

        private string GetQueryString(List<string> queryParams)
        {
            var result = new List<string>();
            foreach (var param in queryParams)
            {
                var p = param;
                if (param[0] == '?')
                {
                    p = param.Substring(1);
                }
                if (_values.ContainsKey(p))
                {
                    result.Add(string.Format("{0}={1}", p, _values[p]));
                }
            }
            if (result.Any())
            {
                return "?" + result.Aggregate((x, y) => x + "&" + y);
            }
            if(queryParams.Any())
            {
                return "{?" + queryParams.Aggregate((x, y) => x + "," + y) + "}";
            }
            return "";
        }

        private List<string> GetQueryParams(Type controllerType)
        {
            var queryParamsAttr = (QueryParametersAttribute)Attribute.GetCustomAttribute(controllerType, typeof(QueryParametersAttribute));
            if (queryParamsAttr == null)
            {
                return new List<string>();
            }
            return queryParamsAttr.Parameters;
        }

        private RouteAttribute GetRouteTemplate(Type controllerType)
        {
            var route = (RouteAttribute)Attribute.GetCustomAttribute(controllerType, typeof(RouteAttribute));

            return route;
        }

        public LinkCreator UseNamedRoute(string routeName, HttpRequestMessage request)
        {
            _request = request;
            _useNamedRoute = true;
            _routeName = routeName;
            return this;
        }

        public LinkCreator WithValues(Dictionary<string, object> values)
        {
            _values = values;
            return this;
        }

        public LinkCreator WithMethod(string verb)
        {
            _httpVerb = verb;
            return this;
        }
    }
}