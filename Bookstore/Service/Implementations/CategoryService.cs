using System;
using Bookstore.Core.Interfaces;
using Bookstore.Repository.Interfaces;
using Bookstore.Service.Interfaces;

namespace Bookstore.Service.Implementations
{
    public class CategoryService : EntityService<Entities.Implementations.Category>, ICategoryService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly ICategoryRepository _categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository): base(unitOfWork,categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
    }
}
