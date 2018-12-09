using System;
using System.Data.Entity;
using Bookstore.Core;
using Bookstore.Entities.Implementations;
using Bookstore.Repository.Interfaces;

namespace Bookstore.Repository.Implementations
{
    public class BookRepository: GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context): base(context)
        {
        }
    }
}
