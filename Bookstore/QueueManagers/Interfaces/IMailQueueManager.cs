using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.QueueManagers.Interfaces
{
    public interface IMailQueueManager
    {
        Task AddMailToQueue(SendGridMessage mail);
        Task AddBulkMailsToQueueAsync(List<SendGridMessage> mails);
    }
}
