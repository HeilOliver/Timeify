using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Timeify.Infrastructure.Context.Identity;

namespace Timeify.Api.StartUp
{
    internal class AuthConfiguration : StartUpConfiguration
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // add api auth
            services.AddAuthorization();

            // add identity
            services
                .AddIdentityCore<AppUser>(o =>
                {
                    // configure identity options
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 6;
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}