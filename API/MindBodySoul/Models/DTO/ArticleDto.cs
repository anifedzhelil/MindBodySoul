using MindBodySoul.Models.Domain;

namespace MindBodySoul.Models.DTO
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public ICollection<ArticleTags>? ArticleTags { get; set; }
    }
}
