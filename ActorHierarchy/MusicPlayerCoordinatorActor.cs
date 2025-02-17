﻿using Akka.Actor;
using System.Collections.Generic;

namespace ActorHierarchy
{
    public class MusicPlayerCoordinatorActor : ReceiveActor
    {
        protected Dictionary<string, IActorRef> MusicPlayerActors;

        public MusicPlayerCoordinatorActor()
        {
            MusicPlayerActors = new Dictionary<string, IActorRef>();
            Receive<PlaySongMessage>(message => PlaySong(message));
            Receive<StopPlayingMessage>(message =>
            StopPlaying(message));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(e =>
            {
                if (e is SongNotAvailableException)
                {
                    return Directive.Resume;
                }
                else if (e is MusicSystemCorruptedException)
                {
                    return Directive.Restart;
                }
                else
                {
                    return Directive.Stop;
                }
            });
        }

        private void StopPlaying(StopPlayingMessage message)
        {
            var musicPlayerActor = GetMusicPlayerActor(message.User);
            if (musicPlayerActor != null)
            {
                musicPlayerActor.Tell(message);
            }
        }
        private void PlaySong(PlaySongMessage message)
        {
            var musicPlayerActor = EnsureMusicPlayerActorExists(message.User);
            musicPlayerActor.Tell(message);
        }

        private IActorRef EnsureMusicPlayerActorExists(string user)
        {
            IActorRef musicPlayerActorReference = GetMusicPlayerActor(user);

            if (musicPlayerActorReference == null)
            {
                //create a new actor's instance.
                musicPlayerActorReference = Context.ActorOf<MusicPlayerActor>(user);

                //add the newly created actor in the dictionary.
                MusicPlayerActors.Add(user, musicPlayerActorReference);
            }
            return musicPlayerActorReference;
        }

        private IActorRef GetMusicPlayerActor(string user)
        {
            IActorRef musicPlayerActorReference;
            MusicPlayerActors.TryGetValue(user, out musicPlayerActorReference);

            return musicPlayerActorReference;
        }
    }
}
