using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Timeify.Common.DI;
using Timeify.Common.Services;
using Timeify.Infrastructure.Interfaces;

namespace Timeify.Infrastructure.Services.Auth
{
    [Injectable(typeof(IJwtTokenHandler), InjectableAttribute.LifeTimeType.Container)]
    internal sealed class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        private readonly ILogger logger;

        public JwtTokenHandler(ILogger logger)
        {
            //if (jwtSecurityTokenHandler == null)
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            this.logger = logger;
        }

        public string WriteToken(JwtSecurityToken jwt)
        {
            return jwtSecurityTokenHandler.WriteToken(jwt);
        }

        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var principal =
                    jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception e)
            {
                logger.LogError($"Token validation failed: {e.Message}");
                return null;
            }
        }
    }
}