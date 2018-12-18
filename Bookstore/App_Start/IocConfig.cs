using Autofac;
using Autofac.Integration.WebApi;
using Bookstore.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Bookstore
{
    public static class IocConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            //
            //var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            //Register Modules
            builder.RegisterModule(new EFModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new AutoMapperModule());

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
        }
    }
}

