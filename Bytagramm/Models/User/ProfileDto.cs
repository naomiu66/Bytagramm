using Bytagramm.Models.Community;

namespace Bytagramm.Models.User
{
    public class ProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<CreateCommunityDto> communities { get; set; }
    }
}
