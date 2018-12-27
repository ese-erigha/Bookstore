using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookstore.Repository.Interfaces;
using Bookstore.Entities.Implementations;
using Bookstore.Core.Implementations;

namespace Bookstore.Repository.Implementations
{
    public class ApiLoggerRepository : GenericRepository<ApiLog>, IApiLoggerRepository
    {
        public ApiLoggerRepository(DatabaseContext context) : base(context)
        {

        }
    }
}