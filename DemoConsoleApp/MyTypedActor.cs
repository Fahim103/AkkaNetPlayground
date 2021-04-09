using Akka.Actor;
using System;

namespace DemoConsoleApp
{
    public class MyTypedActor : ReceiveActor
    {
        public MyTypedActor()
        {
            /* Add Code here */
            /*
                the mapping to the message handlers is done directly in the actor’s constructor
                The mapping is done through one of the many Receive methods to which we have to specify the type and the actual handler
             */

            Receive<GreetingMessage>(message => GreetingMessageHandler(message));
            Receive<string>(message => GreetingMessageHandler(message));
        }

        private void GreetingMessageHandler(GreetingMessage message)
        {
            Console.WriteLine($"Typed Actor named: {Self.Path.Name}");
            Console.WriteLine($"Received a greeting: {message.Greeting}");
            Console.WriteLine($"Actor's path: {Self.Path}");
            Console.WriteLine($"Actor is part of the ActorSystem:{ Context.System.Name}");
        }

        private void GreetingMessageHandler(string message)
        {
            Console.WriteLine(message);
        }
    }
}
