using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Timeify.Api.Shared.Auth;
using Timeify.Common.DI;
using Timeify.Core.Services;
using Timeify.Infrastructure.Context.Identity;
using Timeify.Infrastructure.Interfaces;

namespace Timeify.Infrastructure.Services.Auth
{
    [Injectable(typeof(IJwtFactory), InjectableAttribute.LifeTimeType.Container)]
    internal sealed class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions jwtOptions;
        private readonly IJwtTokenHandler jwtTokenHandler;
        private readonly RoleManager<AppRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public JwtFactory(IJwtTokenHandler jwtTokenHandler, IOptions<JwtIssuerOptions> jwtOptions,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.jwtTokenHandler = jwtTokenHandler;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(this.jwtOptions);
        }

        public async Task<AccessToken> GenerateEncodedToken(string id, string userName)
        {
            // Create the JWT security token and encode it.
            var claims = await GetValidClaims(userName);

            var jwt = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                jwtOptions.NotBefore,
                jwtOptions.Expiration,
                jwtOptions.SigningCredentials);

            return new AccessToken(jwtTokenHandler.WriteToken(jwt), (int) jwtOptions.ValidFor.TotalSeconds);
        }

        private async Task<List<Claim>> GetValidClaims(string userName)
        {
            var appUser = await userManager.FindByNameAsync(userName);

            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64),
                new Claim(options.ClaimsIdentity.UserIdClaimType, appUser.Id),
                new Claim(options.ClaimsIdentity.UserNameClaimType, appUser.UserName)
            };
            var userClaims = await userManager.GetClaimsAsync(appUser);
            var userRoles = await userManager.GetRolesAsync(appUser);
            claims.AddRange(userClaims);
            foreach (string userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims) claims.Add(roleClaim);
                }
            }

            return claims;
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
        {
            return (long) Math.Round((date.ToUniversalTime() -
                                      new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null) throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
}