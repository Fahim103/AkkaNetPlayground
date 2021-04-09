using Akka.Actor;
using System;

namespace AkkaEventBus
{
    public class BookSubscriber : ReceiveActor
    {
        public BookSubscriber()
        {
            Receive<NewBookMessage>(x => HandleNewBookMessage(x));
        }

        private void HandleNewBookMessage(NewBookMessage book)
        {
            Console.WriteLine($"Book: {book.BookName} got published - message received by {Self.Path.Name}!");
        }
    }
}
