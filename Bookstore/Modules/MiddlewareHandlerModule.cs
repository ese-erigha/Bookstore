using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bookstore.CustomHandler;
using Bookstore.CustomHandler.Interfaces;

namespace Bookstore.Modules
{
    public class MiddlewareHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(GlobalExceptionHandler))
                   .As(typeof(IGlobalExceptionHandler))
                   .InstancePerRequest();
        }
    }
}