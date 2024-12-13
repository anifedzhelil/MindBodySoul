
namespace MindBodySoul.Models.DTO
{
    public class ArticleDetailsDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }

        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public required string Content { get; set; }
        public required string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public List<TagDto>? Tags { get; set; } 
    }
}
