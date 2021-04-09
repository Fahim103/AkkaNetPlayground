using System;

namespace Akka.NetCore.WebApi.Messages
{
    public class GetBookById
    {
        public GetBookById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
