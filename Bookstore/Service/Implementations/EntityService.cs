using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bookstore.Core.Interfaces;
using Bookstore.Entities.Implementations;
using Bookstore.Helpers;
using Bookstore.Repository.Interfaces;
using Bookstore.Service.Interfaces;

namespace Bookstore.Service.Implementations
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IGenericRepository<T> _repository;

        protected EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }


        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);

        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);

        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);

        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Commit()
        {
            return await _unitOfWork.Commit();
        }

        public IList<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _repository.FindBy(predicate);
        }

        public PaginationQuery<T> Paginate(PaginationInfo paginationInfo)
        {
            return _repository.Paginate(paginationInfo);
        }

        public async Task<T> FindOneBy(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FindOneBy(predicate);
        }
    }
}
