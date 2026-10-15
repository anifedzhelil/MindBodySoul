using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Models.Domain
{
    public class Article
    {
        public Guid Id { get; set; }
        [MaxLength(150)]
        public required string Title { get; set; }
        [MaxLength(200)] 
        public required string Slug { get; set; }
        public required string ContentText { get; set; }
        public required string Content { get; set; }
        public required Guid SubCategoryId { get; set; }
        public required Guid UserId { get; set; }
        [MaxLength(500)]
        public required string ImageUrl { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool IsArchived { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int TotalVisitCount { get; set; } = 0;
        public int UniqueVisitCount { get; set; } = 0;
        public ICollection<ArticleTags>? ArticleTags { get; set; }
        public ICollection<ArticleVisit>? ArticleVisits { get; set; }
        public SubCategory? SubCategory { get; set; }
    }
}
