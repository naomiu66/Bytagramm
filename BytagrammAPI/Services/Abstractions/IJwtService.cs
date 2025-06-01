using BytagrammAPI.Dto;
using BytagrammAPI.Models;
using System.Security.Claims;

namespace BytagrammAPI.Services.Abstractions
{
    public interface IJwtService
    {
        public Task<TokenDto> GenerateTokens(User user);
        public Task<User> ValidateRefreshTokenAsync(string refreshToken);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
