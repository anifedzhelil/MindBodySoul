namespace MindBodySoul.Models.DTO.Article
{
    public class LatestArticleDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string ImageUrl { get; set; }
        public string Excerpt { get; set; }

    }
}
