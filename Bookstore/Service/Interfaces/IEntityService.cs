using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookstore.Entities.Implementations;
using Bookstore.Helpers;

namespace Bookstore.Service.Interfaces
{
    public interface IEntityService<T>: IService where T : BaseEntity
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        IList<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> FindOneBy(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        Task<T> GetByIdAsync(long Id);
        Task<bool> Commit();
        PaginationQuery<T> Paginate(PaginationInfo paginationInfo);
    }
}
