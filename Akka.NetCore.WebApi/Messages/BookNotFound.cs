namespace Akka.NetCore.WebApi.Messages
{
    public class BookNotFound
    {
        private BookNotFound() { }
        public static BookNotFound Instance { get; } = new BookNotFound();
    }
}
