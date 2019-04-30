using System;
using System.Runtime.Loader;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Timeify.Common.DI;

namespace Timeify.Api.StartUp
{
    public static class ApplicationDiExtension
    {
        public static IServiceProvider AddApplicationDi(this IServiceCollection service)
        {
            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();
            //builder.RegisterModule<Log4NetModule>();

            foreach (var assemblyName in DependencyContext.Default.GetDefaultAssemblyNames())
                AssemblyLoadContext.Default.LoadFromAssemblyName(assemblyName);

            new InjectableMapper()
                .MapHierarchical((source, to) => builder.RegisterType(to).As(source).InstancePerLifetimeScope())
                .MapSingleton((source, to) => builder.RegisterType(to).As(source).SingleInstance())
                .MapTypes();

            builder.Populate(service);
            var container = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }
    }
}