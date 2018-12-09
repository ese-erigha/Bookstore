using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookstore.Entities.Implementations;
using Bookstore.Helpers;

namespace Bookstore.Repository.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll();
        IList<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FindOneBy(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(long id);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
        PaginationQuery<T> Paginate(PaginationInfo paginationInfo);
    }
}
