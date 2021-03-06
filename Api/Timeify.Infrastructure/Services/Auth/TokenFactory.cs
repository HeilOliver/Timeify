﻿using System;
using System.Security.Cryptography;
using Timeify.Common.DI;
using Timeify.Core.Services;

namespace Timeify.Infrastructure.Services.Auth
{
    [Injectable(typeof(ITokenFactory), InjectableAttribute.LifeTimeType.Container)]
    internal sealed class TokenFactory : ITokenFactory
    {
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}