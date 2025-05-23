using Microsoft.AspNetCore.Identity;

namespace BytagrammAPI.Models
{
    public class User : IdentityUser
    {
        public List<Post> Posts { get; set; }
        // Сообщества, которые пользователь создал
        public List<Community> OwnedCommunities { get; set; } = new();

        // Сообщества, на которые пользователь подписан
        public List<Community> SubscribedCommunities { get; set; } = new();
    }
}
