using System;
using System.Net;
using System.Runtime.Loader;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Timeify.Api.Extensions;
using Timeify.Api.StartUp;
using Timeify.Common.DI;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Timeify.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)   
                .AddControllersAsServices();

            new Configurator()
                .AddConfiguration(new DbConfiguration())
                .AddConfiguration(new JwtConfiguration())
                .AddConfiguration(new AuthConfiguration())
                .AddConfiguration(new SwaggerConfiguration())
                .AddConfiguration(new IoCConfiguration())
                .AddConfiguration(new CorsConfiguration())
                .SetConfiguration(Configuration)
                .SetServiceCollection(services)
                .Apply();

            return services
                .AddApplicationDi();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timeify-API V1");
            });
            app.UseSwagger();

            app.UseAuthentication();
            app.UseCors();
            app.UseMvc();
        }
    }
}