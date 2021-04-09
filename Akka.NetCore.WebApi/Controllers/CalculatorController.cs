using Akka.Actor;
using Akka.NetCore.WebApi.Actors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Akka.NetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private ActorSystem _actorSystem;

        public CalculatorController(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }

        [HttpGet]
        public async Task<double> Sum(double x, double y)
        {
            var calculatorActorProps = Props.Create<CalculatorActor>();
            var calculatorRef = _actorSystem.ActorOf(calculatorActorProps);

            AddMessage addMessage = new AddMessage(x, y);
            AnswerMessage answerMessage = await calculatorRef.Ask<AnswerMessage>(addMessage);

            return answerMessage.Value;
        }
    }
}
