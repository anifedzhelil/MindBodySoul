namespace MindBodySoul.Models.Domain
{
    public class Article
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid SubCategoryId { get; set; }
        public required Guid UserId { get; set; }
        public required string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public int TotalVisitCount { get; set; } = 0;
        public int UniqueVisitCount { get; set; } = 0;
        public ICollection<ArticleTags>? ArticleTags { get; set; }
        public ICollection<ArticleVisit>? ArticleVisits { get; set; }
        public SubCategory? SubCategory { get; set; }
    }
}
