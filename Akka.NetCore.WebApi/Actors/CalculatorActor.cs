using Akka.Actor;

namespace Akka.NetCore.WebApi.Actors
{
    public class CalculatorActor : ReceiveActor
    {
        public CalculatorActor()
        {
            Receive<AddMessage>(add => { Sender.Tell(new AnswerMessage(add.Term1 + add.Term2)); });
        }
    }

    public class AddMessage
    {
        public AddMessage(double term1, double term2)
        {
            Term1 = term1;
            Term2 = term2;
        }

        public double Term1;
        public double Term2;
    }

    public class AnswerMessage
    {
        public AnswerMessage(double value)
        {
            Value = value;
        }
        public double Value;
    }
}
