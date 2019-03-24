using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Timeify.Api.StartUp
{
    internal class IoCConfiguration : StartUpConfiguration
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
        }
    }
}