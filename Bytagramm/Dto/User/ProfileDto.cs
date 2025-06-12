using Bytagramm.Dto.Community;

namespace Bytagramm.Dto.User
{
    public class ProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<CommunityDto> Communities { get; set; }
    }
}
