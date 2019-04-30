using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Timeify.Api.Extensions
{
    public static class UsernameExtractor
    {
        public static string GetUsername(this Controller controller)
        {
            string username = controller
                .HttpContext
                .User
                .FindFirst(ClaimTypes.Name).Value;
            return username;
        }
    }
}