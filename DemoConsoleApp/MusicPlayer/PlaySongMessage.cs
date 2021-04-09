namespace DemoConsoleApp.MusicPlayer
{
    public class PlaySongMessage
    {
        public string Song { get; }
        public PlaySongMessage(string song)
        {
            Song = song;
        }
    }

    public class StopPlayingMessage
    {

    }
}
