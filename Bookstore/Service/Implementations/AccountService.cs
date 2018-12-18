using Bookstore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookstore.Entities.Implementations;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Bookstore.Identity;
using System.Data.Entity;

namespace Bookstore.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private  ApplicationUserManager userManager = null;
        private  ApplicationRoleManager roleManager = null;

        public AccountService()
        {
            
        }

        protected ApplicationUserManager _userManager
        {
            get
            {
                return userManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        protected ApplicationRoleManager _roleManager
        {
            get
            {
                return roleManager ?? HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }


        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
           var result =  await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

     
        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            IdentityResult result = null;
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                result = await _roleManager.CreateAsync(new RoleIntPk() { Name = roleName });
            }

            result = await _userManager.AddToRoleAsync(user.Id, roleName);
            return result;
        }

        public async Task<ApplicationUser> FindByIdAsync(long id)
        {
            var user = await GetAllUsers().FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            var manager = _userManager;
            return manager.Users;
        }

        public IQueryable<ApplicationUser> GetUsersByAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return GetAllUsers().Where(predicate);
        }

  

        public async Task<List<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user.Id);
            return roles.ToList();
        }
    }
}