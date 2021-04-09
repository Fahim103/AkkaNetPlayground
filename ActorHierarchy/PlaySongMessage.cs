namespace ActorHierarchy
{
    public class PlaySongMessage
    {
        public string Song { get; }
        public string User { get; }

        public PlaySongMessage(string song, string user)
        {
            Song = song;
            User = user;
        }
    }
}
