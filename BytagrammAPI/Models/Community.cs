using System.ComponentModel.DataAnnotations;

namespace BytagrammAPI.Models
{
    public class Community
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }

        public List<Post> Posts { get; set; }
        public List<User> Subscribers { get; set; }

        public DateTime Created { get; set; }
    }
}
