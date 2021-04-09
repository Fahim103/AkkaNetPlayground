namespace Akka.NetCore.WebApi.Messages
{
    public class GetBooks
    {
        private GetBooks() { }
        public static GetBooks Instance { get; } = new GetBooks();
    }
}
