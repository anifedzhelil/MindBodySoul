namespace MindBodySoul.Models.Domain
{
    public class ArticleVisit
    {
        public Guid Id { get; set; }
        public required Guid ArticleId { get; set; }
        public required Guid UserId { get; set; }
        public required DateTime VisitDate { get; set; }
    }
}
