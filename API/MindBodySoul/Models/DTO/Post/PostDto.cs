namespace MindBodySoul.Models.DTO.Post
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public required string PostName { get; set; }
        public string ThumbnailImage { get; set; } = string.Empty;
        public bool IsPublished { get; set; }   
        public DateTime UpdatedDate { get; set; }
        public int CarouselCounts { get; set; }
    }
}
