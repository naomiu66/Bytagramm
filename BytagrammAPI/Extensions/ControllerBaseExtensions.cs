using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BytagrammAPI.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static string GetUserId(this ControllerBase controller)
        {
            return controller.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserIdFromExpiredToken(this ControllerBase controller, IJwtService tokenService, string accessToken)
        {
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);

            return principal.Claims.First().Value;
        }
    }
}
