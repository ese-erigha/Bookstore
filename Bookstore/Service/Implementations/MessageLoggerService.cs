using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookstore.Service.Interfaces;
using Bookstore.Entities.Implementations;
using Bookstore.Repository.Interfaces;

namespace Bookstore.Service.Implementations
{
    public class MessageLoggerService : IMessageLoggerService
    {
        private readonly IApiLoggerRepository _repository;

        public MessageLoggerService(IApiLoggerRepository repository)
        {
            _repository = repository;
        }

        public void LogMessageAsync(ApiLog apiLog)
        {
            _repository.Add(apiLog);
            _repository.Save();
        }

    }
}