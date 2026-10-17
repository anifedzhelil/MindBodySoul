namespace MindBodySoul.Models.DTO.Article
{
    public class ArticleDetailsDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string Slug { get; set; } = string.Empty;
        public Guid? CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid? UserId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public required string Content { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsPublished { get; set; }                
        public bool IsArchived { get; set; }         
        public int UniqueVisitCount { get; set; }
        public int TotalVisitCount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public List<TagDto>? Tags { get; set; } 
    }
}
