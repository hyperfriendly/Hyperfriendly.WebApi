using System.Net;

namespace Hyperfriendly.WebApi.ErrorHandling
{
    public class ErrorResponse
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }

        public ErrorResponse(HttpStatusCode httpStatusCode, string title, string message)
        {
            HttpStatusCode = httpStatusCode;
            Title = title;
            Message = message;
        }
    }
}