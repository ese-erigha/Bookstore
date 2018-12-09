using System;
using System.Threading.Tasks;

namespace Bookstore.Core.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        Task<bool> Commit();
    }
}
