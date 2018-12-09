using System;
using Bookstore.Core.Interfaces;
using Bookstore.Repository.Interfaces;
using Bookstore.Service.Interfaces;

namespace Bookstore.Service.Implementations
{
    public class AuthorService : EntityService<Entities.Implementations.Author>, IAuthorService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IAuthorRepository _authorRepository;

        public AuthorService(IUnitOfWork unitOfWork, IAuthorRepository authorRepository) : base(unitOfWork,authorRepository)
        {
            _unitOfWork = unitOfWork;
            _authorRepository = authorRepository;
        }
    }
}
