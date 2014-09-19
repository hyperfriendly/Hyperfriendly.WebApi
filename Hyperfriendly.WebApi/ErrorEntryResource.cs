namespace Hyperfriendly.WebApi
{
    public class ErrorEntryResource : Resource
    {
        public string Title { get; private set; }
        public string Message { get; private set; }

        public ErrorEntryResource(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}