using Autofac;
using Bookstore.CustomHandler.Implementations;
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

            builder.RegisterType(typeof(UnhandledExceptionLogger))
                   .As(typeof(IUnhandledExceptionLogger))
                   .InstancePerRequest();

            builder.RegisterType(typeof(RequestResponseHandler)).AsSelf().InstancePerRequest();
        }
    }
}