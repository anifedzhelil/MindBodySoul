namespace MindBodySoul.Models.DTO
{
    public class TagsWithArticleCountDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ArticleCount { get; set; }
    }
}
