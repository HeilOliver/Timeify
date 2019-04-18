using Microsoft.Extensions.DependencyInjection;

namespace Timeify.Api.StartUp
{
    internal class CorsConfiguration : StartUpConfiguration
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        builder.AllowAnyOrigin()
                            .WithExposedHeaders("Token-Expired");
                    });
            });
        }
    }
}