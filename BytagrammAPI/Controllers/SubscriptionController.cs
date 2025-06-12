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

            user.SubscribedCommunities.Add(community);

            List<CommunityDto> dtoList = user.SubscribedCommunities
               .Select(c => new CommunityDto
               {
                   Title = c.Title,
                   Description = c.Description,
               })
               .ToList();

            var communityDto = new CommunityDto
            {
                Id = community.Id,
                AuthorId = community.AuthorId,
                Description = community.Description,
                Title = community.Title
            };

            dtoList.Add(communityDto);

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

            await _userService.UpdateAsync(user);

            return Ok();
        }
    }
}
