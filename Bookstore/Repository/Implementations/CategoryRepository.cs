using System;
using System.Data.Entity;
using Bookstore.Core;
using Bookstore.Entities.Implementations;
using Bookstore.Repository.Interfaces;

namespace Bookstore.Repository.Implementations
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context): base(context)
        {
        }
    }
}
