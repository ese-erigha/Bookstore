using Bookstore.Entities.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Service.Interfaces
{
    public interface IMessageLoggerService : IService
    {
        void LogMessageAsync(ApiLog apiLog);


        

    }
}
