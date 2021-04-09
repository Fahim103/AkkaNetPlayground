using Akka.Actor;
using System;

namespace DemoConsoleApp
{
    public class MyUntypedActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            /* 
                The OnReceive method will be the entry point for all the messages sent to the actor,
                and the implementation of this method should handle all kinds of message types that are expected
            */
            var greeting = message as GreetingMessage;
            if (greeting != null)
            {
                GreetingMessageHandler(greeting);
            }
        }

        private void GreetingMessageHandler(GreetingMessage greeting)
        {
            Console.WriteLine($"Untyped Actor named: {Self.Path.Name}");
            Console.WriteLine($"Received a greeting: {greeting.Greeting}");
            Console.WriteLine($"Actor's path: {Self.Path}");
            Console.WriteLine($"Actor is part of the ActorSystem:{ Context.System.Name}");
        }
    }
}
