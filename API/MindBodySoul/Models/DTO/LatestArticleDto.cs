namespace MindBodySoul.Models.DTO
{
    public class LatestArticleDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string ImageUrl { get; set; }
        public string Excerpt { get; set; }

    }
}
