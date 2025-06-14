using BytagrammAPI.Dto.Post;

namespace BytagrammAPI.Dto.Community
{
    public class CommunityDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public int MembersCount { get; set; } 
        public DateTime CreatedDate { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}
