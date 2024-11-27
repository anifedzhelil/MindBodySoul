namespace MindBodySoul.Models.DTO
{
    public class CreateArticleRequestDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid SubCategoryId { get; set; }
        public required Guid UserId { get; set; }
        public required string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<Guid> TagsIDs { get; set; } = new List<Guid>();

    }
}
