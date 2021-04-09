using Akka.Actor;
using System;

namespace ActorHierarchy
{
    public class MusicPlayerActor : ReceiveActor
    {
        protected PlaySongMessage CurrentSong;
        public MusicPlayerActor()
        {
            StoppedBehavior();
        }
        private void StoppedBehavior()
        {
            Receive<PlaySongMessage>(m => PlaySong(m));
            Receive<StopPlayingMessage>(m => Console.WriteLine($"{m.User}'s player: Cannot stop, the actor is already stopped"));
        }
        private void PlayingBehavior()
        {
            Receive<PlaySongMessage>(m => Console.WriteLine($"{CurrentSong.User}'s player: Cannot play. Currently playing '{CurrentSong.Song}'"));

            Receive<StopPlayingMessage>(m => StopPlaying());
        }

        private void PlaySong(PlaySongMessage message)
        {
            CurrentSong = message;

            if (message.Song == "Bohemian Rhapsody")
            {
                throw new SongNotAvailableException("Bohemian Rhapsody is not available");
            }

            if (message.Song == "Stairway to Heaven")
            {
                throw new MusicSystemCorruptedException("Song in a corrupt state");
            }

            Console.WriteLine($"{CurrentSong.User} is currently listening to '{CurrentSong.Song}'");

            var statsActor = Context.ActorSelection("../../statistics");
            statsActor.Tell(message);


            Become(PlayingBehavior);
        }

        private void StopPlaying()
        {
            Console.WriteLine($"{CurrentSong.User}'s player is currently stopped.");

            CurrentSong = null;
            Become(StoppedBehavior);
        }
    }

    public class SongNotAvailableException : Exception
    {
        public SongNotAvailableException(string message) : base(message)
        {
        }
    }

    public class MusicSystemCorruptedException : Exception
    {
        public MusicSystemCorruptedException(string message) : base(message)
        {
        }
    }
}
