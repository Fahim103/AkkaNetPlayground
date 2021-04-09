using Akka.Actor;
using Akka.NetCore.WebApi.Dto;
using Akka.NetCore.WebApi.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Akka.NetCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IActorRef _booksManagerActor;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BooksManagerActorProvider booksManagerActorProvider, ILogger<BooksController> logger)
        {
            _booksManagerActor = booksManagerActorProvider();
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"Get All books request received. {DateTime.Now}");

            var books = await _booksManagerActor.Ask<IEnumerable<BookDto>>(GetBooks.Instance);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"Get books with Id {id} request received. {DateTime.Now}");

            var result = await _booksManagerActor.Ask(new GetBookById(id));

            return result switch
            {
                BookDto book => Ok(book),
                _ => BadRequest(),
            };

            //switch (result)
            //{
            //    case BookDto book:
            //        return Ok(book);
            //    default:
            //        return BadRequest();
            //}
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateBook command)
        {
            _logger.LogInformation($"Add book request received. {DateTime.Now}");
            _booksManagerActor.Tell(command);
            return Accepted();
        }
    }
}
