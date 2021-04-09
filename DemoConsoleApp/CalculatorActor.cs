using Akka.Actor;

namespace DemoConsoleApp
{
    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            Receive<Add>(add => Sender.Tell(new Answer(add.Term1 + add.Term2)));
        }
    }

    class Answer
    {
        public double Value { get; }
        public Answer(double value)
        {
            Value = value;
        }
    }

    class Add
    {
        public double Term1 { get; }
        public double Term2 { get; }
        public Add(double term1, double term2)
        {
            Term1 = term1;
            Term2 = term2;
        }
    }
}
