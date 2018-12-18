using Bookstore.Core.Implementations;
using Bookstore.Entities.Implementations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser,long>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser,long> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<DatabaseContext>();
            var appUserManager = new ApplicationUserManager(new UserStoreIntPk(appDbContext));

            //Configure validation logic for usernames
            appUserManager.UserValidator = new UserValidator<ApplicationUser,long>(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };


            // Configure validation logic for passwords
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            return appUserManager;
        }
    }
}