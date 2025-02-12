namespace MindBodySoul.Models.DTO
{
    public class CreateArticleVisitDto
    {
        public required Guid ArticleId { get; set; }
        public required Guid UserId { get; set; }
        public required DateTime VisitDate { get; set; }
    }
}
