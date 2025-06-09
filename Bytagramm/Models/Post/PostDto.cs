namespace Bytagramm.Models.Post
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string CommunityId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
