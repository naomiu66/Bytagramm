using BytagrammAPI.Dto.Community;

namespace BytagrammAPI.Models.Redis
{
    public class UserSession
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<CommunityDto> SubscribedCommunities { get; set; }
    }
}
