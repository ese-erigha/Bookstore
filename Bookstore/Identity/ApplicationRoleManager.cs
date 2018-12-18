using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Bookstore.Core.Implementations;
using Bookstore.Entities.Implementations;

namespace Bookstore.Identity
{
    public class ApplicationRoleManager: RoleManager<RoleIntPk, long>
    {
        public ApplicationRoleManager(IRoleStore<RoleIntPk, long> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStoreIntPk(context.Get<DatabaseContext>()));

            return appRoleManager;
        }
    }
}