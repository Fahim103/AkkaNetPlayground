using Akka.NetCore.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace Akka.NetCore.WebApi
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
