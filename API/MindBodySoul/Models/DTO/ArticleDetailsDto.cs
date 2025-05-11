
namespace MindBodySoul.Models.DTO
{
    public class ArticleDetailsDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }

        public Guid? CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid? UserId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public required string Content { get; set; }
        public required string ImageUrl { get; set; }
        public int UniqueVisitCount { get; set; }
        public int TotalVisitCount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public List<TagDto>? Tags { get; set; } 
    }
}
