using Akka.Actor;
using System;

namespace ActorHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("my-first-akka");

            IActorRef dispatcher = system.ActorOf<MusicPlayerCoordinatorActor>("playercoordinator");

            var stats = system.ActorOf<SongPerformanceActor>("statistics");

            dispatcher.Tell(new PlaySongMessage("Smoke on the water", "John"));
            dispatcher.Tell(new PlaySongMessage("Another brick in the wall", "Mike"));
            dispatcher.Tell(new StopPlayingMessage("John"));
            dispatcher.Tell(new StopPlayingMessage("Mike"));
            dispatcher.Tell(new StopPlayingMessage("Mike"));

            dispatcher.Tell(new PlaySongMessage("Bohemian Rhapsody", "John"));
            dispatcher.Tell(new PlaySongMessage("Stairway to Heaven", "Andrew"));

            Console.Read();
            system.Terminate();
        }
    }
}
