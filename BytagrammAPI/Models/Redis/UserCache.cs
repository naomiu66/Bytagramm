using BytagrammAPI.Dto.Community;

namespace BytagrammAPI.Models.Redis
{
    public class UserCache : ICachable
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public required List<CommunityDto> SubscribedCommunities { get; set; }
    }
}
