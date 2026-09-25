namespace MindBodySoul.Models.DTO.Post
{
    public class UpdatePostRequestDto
    {
        public required string PostName { get; set; }
        public bool IsPublished { get; set; }
    }
}
