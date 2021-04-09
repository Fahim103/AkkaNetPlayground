using Akka.Actor;
using Akka.NetCore.WebApi.Domain;
using Akka.NetCore.WebApi.Dto;
using Akka.NetCore.WebApi.Extensions;
using Akka.NetCore.WebApi.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Akka.NetCore.WebApi.Actors
{
    public class BooksManagerActor : ReceiveActor
    {
        private readonly Dictionary<Guid, Book> _books = new Dictionary<Guid, Book>()
        {
            {
                Guid.Parse("F1659B74-2BA8-4A48-98DB-A7E21D5F4D0E"),
                new Book
                {
                    Id=Guid.Parse("F1659B74-2BA8-4A48-98DB-A7E21D5F4D0E"),
                    Title = "Book 1",
                    Author = "Author 1",
                    Cost = 12.99M,
                    InventoryAmount = 10
                }
            },
            {
                Guid.Parse("E7A48654-07A9-44CB-8261-9DD6DDBE24C2"),
                new Book
                {
                    Id=Guid.Parse("E7A48654-07A9-44CB-8261-9DD6DDBE24C2"),
                    Title = "Book 2",
                    Author = "Author 2",
                    Cost = 15.99M,
                    InventoryAmount = 20
                }
            },
            {
                Guid.Parse("D6AEC294-3900-4F14-9093-77C23434380C"),
                new Book
                {
                    Id=Guid.Parse("D6AEC294-3900-4F14-9093-77C23434380C"),
                    Title = "Book 3",
                    Author = "Author 3",
                    Cost = 18.99M,
                    InventoryAmount = 12
                }
            }
        };

        public BooksManagerActor()
        {
            //Receive<CreateBook>(command => AddBookHandler(command));

            //Receive<GetBookById>(query => GetBookByIdHandler(query));

            //Receive<GetBooks>(query => GetBooksHandler(query));

            Receive<CreateBook>(command => AddBookToDbHandler(command));

            Receive<GetBookById>(query => GetBookByIdFromDbHandler(query));

            Receive<GetBooks>(query => GetBooksFromDbHandler(query));
        }

        private void AddBookToDbHandler(CreateBook command)
        {
            using (IServiceScope serviceScope = Context.CreateScope())
            {
                var bookDbContext = serviceScope.ServiceProvider.GetService<BookDbContext>();
                var newBook = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = command.Title,
                    Author = command.Author,
                    Cost = command.Cost,
                    InventoryAmount = command.InventoryAmount,
                };

                bookDbContext.Books.Add(newBook);
                bookDbContext.SaveChanges();
            }
        }

        private void GetBooksFromDbHandler(GetBooks query)
        {
            using (IServiceScope serviceScope = Context.CreateScope())
            {
                var bookDbContext = serviceScope.ServiceProvider.GetService<BookDbContext>();
                var books = bookDbContext.Books.ToList();
                Sender.Tell(books.Select(x => GetBookDto(x)).ToList());
            }
        }

        private void GetBookByIdFromDbHandler(GetBookById query)
        {
            using (IServiceScope serviceScope = Context.CreateScope())
            {
                var bookDbContext = serviceScope.ServiceProvider.GetService<BookDbContext>();
                var book = bookDbContext.Books.Where(x => x.Id == query.Id).FirstOrDefault();

                if (book is not null)
                {
                    Sender.Tell(GetBookDto(book));
                }
                else
                {
                    Sender.Tell(BookNotFound.Instance);
                }
            }
        }

        private void AddBookHandler(CreateBook command)
        {
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Author = command.Author,
                Cost = command.Cost,
                InventoryAmount = command.InventoryAmount,
            };

            _books.Add(newBook.Id, newBook);
        }

        private void GetBookByIdHandler(GetBookById query)
        {
            if (_books.TryGetValue(query.Id, out var book))
                Sender.Tell(GetBookDto(book));
            else
                Sender.Tell(BookNotFound.Instance);
        }

        private void GetBooksHandler(GetBooks query)
        {
            Sender.Tell(_books.Select(x => GetBookDto(x.Value)).ToList());
        }

        private static BookDto GetBookDto(Book book) => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Cost = book.Cost,
            InventoryAmount = book.InventoryAmount
        };
    }
}
