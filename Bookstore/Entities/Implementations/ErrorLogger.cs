using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Entities.Implementations
{
    public class ErrorLogger : BaseEntity
    {
        public string Message { get; set; }

        public string RequestMethod { get; set; }

        public string RequestUri { get; set; }

        public DateTime TimeUtc { get; set; }
    }
}