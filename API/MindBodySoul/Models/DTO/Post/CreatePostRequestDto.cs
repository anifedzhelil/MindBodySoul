namespace MindBodySoul.Models.DTO.Post
{
    public class CreatePostRequestDto
    {
        public required string PostName { get; set; }

        public bool isPublished { get; set; }
        
    }
}
