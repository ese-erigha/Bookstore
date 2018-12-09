using System;
using Bookstore.Core.Interfaces;
using Bookstore.Entities.Implementations;
using Bookstore.Repository.Interfaces;
using Bookstore.Service.Interfaces;

namespace Bookstore.Service.Implementations
{
    public class BookService : EntityService<Book>, IBookService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IBookRepository _bookRepository;

        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository): base(unitOfWork,bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }
    }
}
