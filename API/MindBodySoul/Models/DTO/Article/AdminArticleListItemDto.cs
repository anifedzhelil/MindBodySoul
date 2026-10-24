namespace MindBodySoul.Models.DTO.Article
{
    public class AdminArticleListItemDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsPublished { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int TotalVisitCount { get; set; }
        public int UniqueVisitCount { get; set; }
    }

}
