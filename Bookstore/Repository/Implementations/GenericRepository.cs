using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookstore.Core;
using Bookstore.Entities.Implementations;
using Bookstore.Helpers;
using Bookstore.Repository.Interfaces;

namespace Bookstore.Repository.Implementations
{
    public abstract class GenericRepository<T>: IGenericRepository<T> where T: BaseEntity
    {
        protected DatabaseContext _context;
        protected readonly IDbSet<T> _dbSet;

        protected GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IList<T> FindBy(Expression<Func<T, bool>> where)
        {

            IList<T> query = _dbSet.Where(where).ToList();
            return query;
        }

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public PaginationQuery<T> Paginate(PaginationInfo paginationInfo)
        {
            var queryAble = _dbSet.AsQueryable();
            var count = queryAble.Count();

            var items = queryAble
                         .OrderBy(x => x.UpdatedDate)
                         .Skip((paginationInfo.PageNumber - 1) * paginationInfo.PageSize)
                         .Take(paginationInfo.PageSize)
                         .ToList();

            return new PaginationQuery<T> { Count = count, Items = items };
        }

        public async Task<T> FindOneBy(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).SingleOrDefaultAsync();
        }
    }
}
