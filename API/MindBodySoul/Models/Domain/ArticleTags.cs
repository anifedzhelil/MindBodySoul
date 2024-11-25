namespace MindBodySoul.Models.Domain
{
    public class ArticleTags
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
        public required Article Article { get; set; }
        public required Tag Tag { get; set; }

    }
}
