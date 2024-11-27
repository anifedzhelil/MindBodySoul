namespace MindBodySoul.Models.Domain
{
    public class ArticleTags
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
        public  Article? Article { get; set; }
        public  Tag? Tag { get; set; }

    }
}
