namespace MindBodySoul.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<ArticleTags>? ArticleTags { get; set; }

    }
}
