namespace DemoConsoleApp
{
    public class GreetingMessage
    {
        public string Greeting { get; private set; }

        public GreetingMessage(string greeting)
        {
            Greeting = greeting;
        }
    }
}
