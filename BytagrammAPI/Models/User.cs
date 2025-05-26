using Microsoft.AspNetCore.Identity;

namespace BytagrammAPI.Models
{
    public class User : IdentityUser
    {
        public List<Post> Posts { get; set; }
        public List<Community> OwnedCommunities { get; set; } = new();
        public List<Community> SubscribedCommunities { get; set; } = new();
    }
}
