using Akka.Actor;
using DemoConsoleApp.MusicPlayer;
using System;
using System.Threading.Tasks;

namespace DemoConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

        }

        static async Task MusicPlayerAndActorRoleChange()
        {
            ActorSystem actorSystem = ActorSystem.Create("musicplayer-system");

            IActorRef musicPlayerActor = actorSystem.ActorOf<MusicPlayerActor>("musicPlayer");

            musicPlayerActor.Tell(new PlaySongMessage("Smoke on the water"));
            musicPlayerActor.Tell(new PlaySongMessage("Another brick in the wall"));
            musicPlayerActor.Tell(new StopPlayingMessage());
            musicPlayerActor.Tell(new StopPlayingMessage());
            musicPlayerActor.Tell(new PlaySongMessage("Another brick in the wall"));

            Console.ReadLine();

            await actorSystem.Terminate();
        }

        static async Task CalculatorDemo()
        {
            ActorSystem actorSystem = ActorSystem.Create("calculator-system");

            IActorRef calculatorActor = actorSystem.ActorOf<CalculatorActor>("calculator");

            //var result = calculatorActor.Ask<Answer>(new Add(1, 2)).Result;

            var result = await calculatorActor.Ask<Answer>(new Add(1, 2));


            Console.WriteLine($"Add result is : {result.Value}");

            await actorSystem.Terminate();

            Console.ReadLine();
        }

        static void FirstActorDemo()
        {
            ActorSystem actorSystem = ActorSystem.Create("my-first-akka");

            // Create actors with Generic Function
            IActorRef typedActor = actorSystem.ActorOf<MyTypedActor>("typed-actor");
            IActorRef untypedActor = actorSystem.ActorOf<MyUntypedActor>("untyped-actor");

            // Create actors with Non-Generic Function
            Props typedActorProps = Akka.Actor.Props.Create<MyTypedActor>();
            Props untypedActorProps = Akka.Actor.Props.Create<MyUntypedActor>();

            IActorRef typedActorFromProp = actorSystem.ActorOf(typedActorProps);
            IActorRef untypedActorFromProp = actorSystem.ActorOf(untypedActorProps);


            typedActor.Tell(new GreetingMessage("Typed Actor"));
            untypedActor.Tell(new GreetingMessage("Typed Actor"));

            Console.ReadLine();

            actorSystem.Terminate();
        }
    }
}
