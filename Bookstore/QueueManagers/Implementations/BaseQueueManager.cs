using Microsoft.Azure;
using Microsoft.Azure.ServiceBus;

namespace Bookstore.QueueManagers.Implementations
{
    public class BaseQueueManager : AbstractQueueManager
    {
        private string ServiceBusConnectionString;
        public IQueueClient queueClient;

        public BaseQueueManager(string queueName)
        {
            QueueName = queueName;
        }

        public IQueueClient GetQueueClient()
        {
            ServiceBusConnectionString = "<Namespace connection String>"; // get it from azure portal from service bus namespace shared                                                                           //access policy 
            queueClient = new QueueClient(ServiceBusConnectionString,QueueName);
            return queueClient;
        }
    }
}