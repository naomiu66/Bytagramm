using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BytagrammAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public List<Post> Posts { get; set; }

        [Required]
        public List<Community> OwnedCommunities { get; set; } = new();

        [Required]
        public List<Community> SubscribedCommunities { get; set; } = new();
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? RefreshToken { get; set; }
    }
}
