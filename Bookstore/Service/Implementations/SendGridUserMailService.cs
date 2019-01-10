using Bookstore.Model.MailDto;
using Bookstore.QueueManagers.Interfaces;
using Bookstore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Service.Implementations
{
    public class SendGridUserMailService : IUserMailService
    {
        private readonly IMailQueueManager _mailQueueManager;

        public SendGridUserMailService(IMailQueueManager mailQueueManager)
        {
            _mailQueueManager = mailQueueManager;
        }

        public void SendAccountCreatedMail(UserAccountCreatedToken user)
        {
            throw new NotImplementedException();
        }

        public void SendPasswordResetMail(UserPasswordChangedToken user)
        {
            throw new NotImplementedException();
        }
    }
}