using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Hyperfriendly.WebApi.ErrorHandling
{
    public class HyperfriendlyExceptionHandler : ExceptionHandler
    {
        private readonly ExceptionTransformer _transformer;

        public HyperfriendlyExceptionHandler(ExceptionTransformer transformer)
        {
            _transformer = transformer;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            var errorResponse = _transformer.Transform(context.Exception);
            var errorResource = new ErrorResource();
            errorResource.Errors.Add(new ErrorEntryResource(errorResponse.Title, errorResponse.Message));
            var resp = new HttpResponseMessage(errorResponse.HttpStatusCode)
            {
                Content = new ObjectContent(typeof(ErrorResource), errorResource, new HyperfriendlyJsonMediaTypeFormatter())
            };

            context.Result = new HyperfriendlyErrorMessageResult(resp);
        }

        public class HyperfriendlyErrorMessageResult : IHttpActionResult
        {
            private readonly HttpResponseMessage _httpResponseMessage;

            public HyperfriendlyErrorMessageResult(HttpResponseMessage httpResponseMessage)
            {
                _httpResponseMessage = httpResponseMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                return Task.FromResult(_httpResponseMessage);
            }
        }
    }
}