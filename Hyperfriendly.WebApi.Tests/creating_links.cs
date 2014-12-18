using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hyperfriendly.WebApi.Example;
using Should;
using Xunit;

namespace Hyperfriendly.WebApi.Tests
{
    public class creating_links
    {
        [Fact]
        public void to_controller_with_attribute_route()
        {
            var link = LinkCreator.Create("attribute_routing")
                .GetLink<RouteAttributeControllers>();

            link.Href.ShouldEqual("RouteAttr");
        }

        [Fact]
        public void to_controller_with_attribute_route_and_parameters()
        {
            var link = LinkCreator.Create("attribute_routing_with_param")
                .WithValues(new Dictionary<string, object>{{"p1", 123},{"p2", "foo"}})
                .GetLink<RouteAttributeWithParamController>();

            link.Href.ShouldEqual("RouteAttrWithParam/123/foo");
        }

        [Fact]
        public void to_controller_with_attribute_route_and_conditional_parameters()
        {
            var link = LinkCreator.Create("attribute_routing_with_param")
                .GetLink<RouteAttributeWithCondirtionalParamController>();

            link.Href.ShouldEqual("RouteAttrWithConditionalParam/{p1}");
        }


        [Fact]
        public void to_controller_with_optional_route_parameter_set()
        {
            var link = LinkCreator.Create("attribute_routing_with_param_not_set")
                            .WithValues(new Dictionary<string, object>{{"p1", 1}})
                            .GetLink<RouteAttributeWithOptionalParamController>();

            link.Href.ShouldEqual("RouteAttrWithOptionalParam/1");
        }

        [Fact]
        public void to_controller_with_optional_route_parameter_not_set()
        {
            var link = LinkCreator.Create("attribute_routing_with_param_not_set")
                .GetLink<RouteAttributeWithOptionalParamController>();

            link.Href.ShouldEqual("RouteAttrWithOptionalParam/{p1}");
        }

        [Fact]
        public void to_controller_with_query_parameter()
        {
            var link = LinkCreator.Create("attribute_routing_with_query_parameter")
                .WithValues(new Dictionary<string, object>{{"p1", 1}})
                .GetLink<RouteAttributeWithQueryParamController>();

            link.Href.ShouldEqual("RouteAttrWithQueryParam?p1=1");
        }

        [Fact]
        public void to_controller_with_optional_query_parameter()
        {
            var link = LinkCreator.Create("attribute_routing_with_query_parameter")
                .GetLink<RouteAttributeWithQueryParamController>();

            link.Href.ShouldEqual("RouteAttrWithQueryParam{?p1,p2}");
        }

        [Fact]
        public void to_controller_with_optional_query_parameter_specified()
        {
            var link = LinkCreator.Create("attribute_routing_with_query_parameter")
                .WithValues(new Dictionary<string, object> { { "p2", 2 }, {"p1", 1} })
                .GetLink<RouteAttributeWithQueryParamController>();

            link.Href.ShouldEqual("RouteAttrWithQueryParam?p1=1&p2=2");
        }
        [Fact]
        public void to_controller_with_everything()
        {
            var link = LinkCreator.Create("attribute_routing_with_query_parameter")
                .WithValues(new Dictionary<string, object> { {"p1", 1}, { "p2", 2 }})
                .GetLink<RouteAttributeWithEverythingController>();

            link.Href.ShouldEqual("RouteAttrWithEVERYTHING/1?p1=1&p2=2");
        }
    }

}