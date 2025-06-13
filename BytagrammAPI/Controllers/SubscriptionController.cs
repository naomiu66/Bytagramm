using BytagrammAPI.Dto.Community;
using BytagrammAPI.Dto.Subscriptions;
using BytagrammAPI.Extensions;
using BytagrammAPI.Models;
using BytagrammAPI.Models.Redis;
using BytagrammAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BytagrammAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICommunityService _communityService;
        private readonly ICacheService _cacheService;

        public SubscriptionController(IUserService userService, ICommunityService communityService, ICacheService cacheService)
        {
            _userService = userService;
            _communityService = communityService;
            _cacheService = cacheService;
        }

        [Authorize]
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] NewCommunitySubscription newCommunitySubscription) 
        {
            var userId = this.GetUserId();

            var user = await _userService.GetByIdAsync(userId);

            var community = await _communityService.GetByIdAsync(newCommunitySubscription.CommunityId);

            if (!user.SubscribedCommunities.Any(c => c.Id == community.Id))
            {
                user.SubscribedCommunities.Add(community);
            }


            await _userService.UpdateAsync(user);

            List<CommunityDto> dtoList = user.SubscribedCommunities
               .Select(c => new CommunityDto
               {
                   Id = c.Id,
                   Title = c.Title,
                   Description = c.Description,
                   AuthorId = c.AuthorId,
               })
               .ToList();

            var cachedUser = new Cache<UserCache>
            {
                Key = userId,
                Payload = new UserCache
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    SubscribedCommunities = dtoList
                }
            };

            await _cacheService.SetAsync(cachedUser);

            return Ok();
        }
    }
}
