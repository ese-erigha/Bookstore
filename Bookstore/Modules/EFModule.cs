using System;
using System.Data.Entity;
using Autofac;
using Bookstore.Core;
using Bookstore.Core.Implementations;
using Bookstore.Core.Interfaces;

namespace Bookstore.Modules
{
    public class EFModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterType(typeof(DatabaseContext)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
        }
    }
}
