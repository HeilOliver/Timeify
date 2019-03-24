using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Timeify.Api.StartUp
{
    internal abstract class StartUpConfiguration
    {
        public IConfiguration Configuration { get; set; }

        public abstract void ConfigureServices(IServiceCollection services);
    }
}