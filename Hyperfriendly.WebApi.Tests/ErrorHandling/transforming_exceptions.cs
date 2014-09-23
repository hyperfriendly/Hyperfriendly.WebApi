using System;
using System.Net;
using Hyperfriendly.WebApi.ErrorHandling;
using Should;
using Xunit;

namespace Hyperfriendly.WebApi.Tests.ErrorHandling
{
    public class transforming_exceptions
    {
        [Fact]
        public void transforms_given_exception_to_error_response()
        {
            const string title = "Foo title";
            const string message = "Foo message";
            const HttpStatusCode statusCode = HttpStatusCode.NotFound;

            var transformer = new ExceptionTransformer();
            transformer.AddTransformer<ArgumentException>(e =>
            {
                return new ErrorResponse(statusCode, title, e.Message);
            });

            var errorResponse = transformer.Transform(new ArgumentException(message));

            errorResponse.Title.ShouldEqual(title);
            errorResponse.Message.ShouldEqual(message);
            errorResponse.HttpStatusCode.ShouldEqual(statusCode);
        }

        [Fact]
        public void falls_back_to_default_handler()
        {
            const string title = "An error occurred.";
            const string message = "An error occurred. Please contact help desk.";
            const HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            var transformer = new ExceptionTransformer
            {
                DefaultTransformer = e => new ErrorResponse(statusCode, title, message)
            };

            var errorResponse = transformer.Transform(new NullReferenceException());

            errorResponse.Title.ShouldEqual(title);
            errorResponse.Message.ShouldEqual(message);
            errorResponse.HttpStatusCode.ShouldEqual(statusCode);            
        }

        [Fact]
        public void throws_when_transformer_is_not_defined_for_a_type_and_no_default_transformer_is_defined()
        {
            var transformer = new ExceptionTransformer();

            var exception = Assert.Throws<ApplicationException>(() => transformer.Transform(new Exception()));

            exception.Message.ShouldContain("No tranformer is defined for type \"Exception\"");
        }
    }
}