using Microsoft.AspNetCore.Identity;

namespace MindBodySoul.Models.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; } 
        public required string Content { get; set; } 
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        public DateTime? UpdatedDate { get; set; }
        public required Guid ArticleId { get; set; } 
        public required Guid UserId { get; set; } 
    
        public string? UserName {  get; set; }
    }
}
