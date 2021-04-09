namespace AkkaEventBus
{
    public class NewBookMessage
    {
        public NewBookMessage(string name)
        {
            BookName = name;
        }
        public string BookName { get; }
    }
}
