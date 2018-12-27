using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Bookstore.CustomHandler.Interfaces
{
    public interface IUnhandledExceptionLogger
    {
        void Log(ExceptionLoggerContext context);
    }
}
