using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Timeify.Infrastructure.Context.Identity;

namespace Timeify.Api.StartUp
{
    internal class AuthConfiguration : StartUpConfiguration
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // api user claim policy
            services.AddAuthorization(options =>
            {
                // Here I stored necessary permissions/roles in a constant
                //foreach (var prop in typeof(ClaimPermission).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                //{
                //    options.AddPolicy(prop.GetValue(null).ToString(), policy => policy.RequireClaim(ClaimType.Permission, prop.GetValue(null).ToString()));
                //}
                //options
                //    .AddPolicy("ApiUser", policy => policy
                //        .RequireClaim(StaticJwtClaimIdentifiers.Rol, StaticJwtClaims.ApiAccess));
            });

            //services.AddIdentityWithoutAuthenticator<AppUser, AppRole>(o =>
            //{
            //    // configure identity options
            //    o.Password.RequireDigit = false;
            //    o.Password.RequireLowercase = false;
            //    o.Password.RequireUppercase = false;
            //    o.Password.RequireNonAlphanumeric = false;
            //    o.Password.RequiredLength = 6;
            //});

            // add identity
            var identityBuilder = services
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

            //identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(AppRole), identityBuilder.Services);
            //identityBuilder
            //    .AddEntityFrameworkStores<AppIdentityDbContext>()
            //    .AddDefaultTokenProviders();

            //identityBuilder.AddRoleValidator<RoleValidator<AppRole>>();
            //identityBuilder.AddRoleManager<RoleManager<AppRole>>();
            //identityBuilder.AddSignInManager<SignInManager<AppUser>>();
        }
    }

    /// <summary>
    ///     Contains extension methods to <see cref="IServiceCollection" /> for configuring identity services.
    /// </summary>
    public static class IdentityServiceExtensions
    {
        /// <summary>
        ///     Adds the default identity system configuration for the specified User and Role types. (Without Authentication
        ///     Scheme)
        /// </summary>
        /// <typeparam name="TUser">The type representing a User in the system.</typeparam>
        /// <typeparam name="TRole">The type representing a Role in the system.</typeparam>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IdentityBuilder" /> for creating and configuring the identity system.</returns>
        public static IdentityBuilder AddIdentityWithoutAuthenticator<TUser, TRole>(this IServiceCollection services)
            where TUser : class
            where TRole : class
        {
            return services.AddIdentityWithoutAuthenticator<TUser, TRole>(null);
        }

        /// <summary>
        ///     Adds and configures the identity system for the specified User and Role types. (Without Authentication Scheme)
        /// </summary>
        /// <typeparam name="TUser">The type representing a User in the system.</typeparam>
        /// <typeparam name="TRole">The type representing a Role in the system.</typeparam>
        /// <param name="services">The services available in the application.</param>
        /// <param name="setupAction">An action to configure the <see cref="IdentityOptions" />.</param>
        /// <returns>An <see cref="IdentityBuilder" /> for creating and configuring the identity system.</returns>
        public static IdentityBuilder AddIdentityWithoutAuthenticator<TUser, TRole>(this IServiceCollection services,
            Action<IdentityOptions> setupAction)
            where TUser : class
            where TRole : class
        {
            // Hosting doesn't add IHttpContextAccessor by default
            services.AddHttpContextAccessor();
            // Identity services
            services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
            services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
            services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<TRole>, RoleValidator<TRole>>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<TUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser, TRole>>();
            services.TryAddScoped<UserManager<TUser>>();
            services.TryAddScoped<SignInManager<TUser>>();
            services.TryAddScoped<RoleManager<TRole>>();

            if (setupAction != null) services.Configure(setupAction);

            return new IdentityBuilder(typeof(TUser), typeof(TRole), services);
        }
    }
}