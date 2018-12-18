using Bookstore.Entities.Implementations;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Service.Interfaces
{
    public interface IAccountService : IService
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<ApplicationUser> FindByIdAsync(long id);

        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName);

        IQueryable<ApplicationUser> GetAllUsers();

        IQueryable<ApplicationUser> GetUsersByAsync(Expression<Func<ApplicationUser, bool>> predicate);

        Task<List<string>> GetRolesAsync(ApplicationUser user);

    }
}
