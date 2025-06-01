using System.ComponentModel.DataAnnotations;

namespace BytagrammAPI.Models
{
    public class Post
    {
        [Required]
        public string Id { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }
        public User Author { get; set; }

        public Community Community { get; set; }
        public string CommunityId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
