using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Entities.Implementations;

namespace Bookstore.Service.Interfaces
{
    public interface IUserNotificationService : IService
    {
        void Registered(ApplicationUser user);
        void PasswordChanged(ApplicationUser user);
    }
}
