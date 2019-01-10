using Bookstore.QueueManagers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.QueueManagers.Implementations
{
    public abstract class AbstractQueueManager : IQueueManager
    {
        public string QueueName { get; set; }
    }
}