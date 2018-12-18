using System;
using Bookstore.Entities.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Bookstore.Entities.Implementations
{
    public class ApplicationUser : IdentityUser<long, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set ; }
        public bool IsDeleted { get; set ; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,long> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}