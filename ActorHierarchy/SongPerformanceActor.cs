using Akka.Actor;
using System;
using System.Collections.Generic;

namespace ActorHierarchy
{
    public class SongPerformanceActor : ReceiveActor
    {
        protected Dictionary<string, int> SongPeformanceCounter;
        public SongPerformanceActor()
        {
            SongPeformanceCounter = new Dictionary<string, int>();
            Receive<PlaySongMessage>(m => IncreaseSongCounter(m));
        }
        public void IncreaseSongCounter(PlaySongMessage m)
        {
            var counter = 1;

            if (SongPeformanceCounter.ContainsKey(m.Song))
            {
                counter = SongPeformanceCounter[m.Song]++;
            }
            else
            {
                SongPeformanceCounter.Add(m.Song, counter);
            }

            Console.WriteLine($"Song: {m.Song} has been played {counter} times");
        }

    }
}
