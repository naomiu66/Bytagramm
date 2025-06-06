using BytagrammAPI.Dto.Community;

namespace BytagrammAPI.Dto.User
{
    public class ProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<CreateCommunityDto> Communities { get; set; }
    }
}
