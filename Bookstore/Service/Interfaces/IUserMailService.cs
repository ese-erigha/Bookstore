using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Model.MailDto;

namespace Bookstore.Service.Interfaces
{
    public interface IUserMailService : IService
    {
        void SendAccountCreatedMail(UserAccountCreatedToken user);
        void SendPasswordResetMail(UserPasswordChangedToken user);
    }
}
