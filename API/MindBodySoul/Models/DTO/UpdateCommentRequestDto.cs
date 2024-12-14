namespace CodePulse.API.Models.DTO
{
    public class UpdateCommentRequestDto
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

