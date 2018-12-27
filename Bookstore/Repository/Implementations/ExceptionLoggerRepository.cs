using Bookstore.Entities.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookstore.Repository.Interfaces;
using Bookstore.Core.Implementations;

namespace Bookstore.Repository.Implementations
{
    public class ExceptionLoggerRepository : GenericRepository<ErrorLogger>, IExceptionLoggerRepository
    {
        public ExceptionLoggerRepository(DatabaseContext context): base(context)
        {
        }
    }
}