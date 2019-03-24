﻿using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Timeify.Common.DI;
using Timeify.Core.Services;
using Timeify.Infrastructure.Interfaces;

namespace Timeify.Infrastructure.Auth
{
    [Injectable(typeof(IJwtTokenValidator), InjectableAttribute.LifeTimeType.Hierarchical)]
    internal sealed class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly IJwtTokenHandler jwtTokenHandler;

        public JwtTokenValidator(IJwtTokenHandler jwtTokenHandler)
        {
            this.jwtTokenHandler = jwtTokenHandler;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey)
        {
            return jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = false // we check expired tokens here
            });
        }
    }
}