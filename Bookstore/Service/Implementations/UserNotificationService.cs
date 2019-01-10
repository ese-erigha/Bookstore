using Bookstore.Entities.Implementations;
using Bookstore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Service.Implementations
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUserMailService _sendGridMailService;

        public UserNotificationService(IUserMailService sendGridMailService)
        {
            _sendGridMailService = sendGridMailService;
        }

        public void PasswordChanged(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Registered(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}