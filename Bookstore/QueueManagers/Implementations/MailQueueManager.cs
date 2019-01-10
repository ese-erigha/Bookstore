using Bookstore.QueueManagers.Interfaces;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.QueueManagers.Implementations
{
    public class MailQueueManager : BaseQueueManager, IMailQueueManager
    {
        public MailQueueManager() : base("QueueName")
        {
            
        }

        public Task AddBulkMailsToQueueAsync(List<SendGridMessage> mails)
        {
            throw new NotImplementedException();
        }

        public async Task AddMailToQueue(SendGridMessage mail)
        {
            IQueueClient queueClient = GetQueueClient();
            var msgBody = JsonConvert.SerializeObject(mail);
            var msg = new Message(Encoding.UTF8.GetBytes(msgBody));
            await queueClient.SendAsync(msg);
        }
    }
}