namespace MindBodySoul.Models.DTO
{
    public class CreateCommentRequestDto
    {
        public required string Content { get; set; }
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public required Guid ArticleId { get; set; }
        public required Guid UserId { get; set; }

    }
}
