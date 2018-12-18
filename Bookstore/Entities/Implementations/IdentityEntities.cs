using Bookstore.Core.Implementations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Bookstore.Entities.Implementations
{
    //New drived classes 
    public class UserRoleIntPk : IdentityUserRole<long>
    {
    }

    public class UserClaimIntPk : IdentityUserClaim<long>
    {
    }

    public class UserLoginIntPk : IdentityUserLogin<long>
    {
    }

    public class RoleIntPk : IdentityRole<long, UserRoleIntPk>
    {
        public RoleIntPk() { }
        public RoleIntPk(string name) { Name = name; }
    }

    public class UserStoreIntPk : UserStore<ApplicationUser, RoleIntPk, long,
        UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public UserStoreIntPk(DatabaseContext context)
            : base(context)
        {
        }
    }

    public class RoleStoreIntPk : RoleStore<RoleIntPk, long, UserRoleIntPk>
    {
        public RoleStoreIntPk(DatabaseContext context)
            : base(context)
        {
        }
    }
}