using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Timeify.Api.StartUp
{
    internal class Configurator
    {
        private readonly List<StartUpConfiguration> configurations
            = new List<StartUpConfiguration>();

        private IConfiguration configuration;
        private IServiceCollection serviceCollection;

        public Configurator AddConfiguration(StartUpConfiguration config)
        {
            configurations.Add(config);
            return this;
        }

        public Configurator SetServiceCollection(IServiceCollection services)
        {
            serviceCollection = services;
            return this;
        }

        public Configurator SetConfiguration(IConfiguration config)
        {
            configuration = config;
            return this;
        }

        public void Apply()
        {
            foreach (var upConfiguration in configurations)
            {
                upConfiguration.Configuration = configuration;
                upConfiguration.ConfigureServices(serviceCollection);
            }
        }
    }
}