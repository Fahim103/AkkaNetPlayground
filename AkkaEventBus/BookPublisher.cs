using Akka.Actor;

namespace AkkaEventBus
{
    public class BookPublisher : ReceiveActor
    {
        public BookPublisher()
        {
            Receive<NewBookMessage>(x => Handle(x));
        }
        private void Handle(NewBookMessage x)
        {
            Context.System.EventStream.Publish(x);
        }
    }
}
