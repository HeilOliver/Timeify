using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timeify.Infrastructure.Context.App;
using Timeify.Infrastructure.Context.Identity;

namespace Timeify.Api.StartUp
{
    internal class DbConfiguration : StartUpConfiguration
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<AppIdentityDbContext>(
                options => options
                    .UseSqlServer(Configuration.GetConnectionString("Default"),
                        b => b.MigrationsAssembly("Timeify.Infrastructure")));

            services.AddDbContext<AppDbContext>(
                options => options
                    .UseSqlServer(Configuration.GetConnectionString("Default"),
                        b => b.MigrationsAssembly("Timeify.Infrastructure")));
        }
    }
}