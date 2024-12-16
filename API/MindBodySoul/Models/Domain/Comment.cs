using Microsoft.AspNetCore.Identity;

namespace MindBodySoul.Models.Domain
{
    public class Comment
    {
        public Guid Id { get; set; } 
        public required string Content { get; set; } 
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        public DateTime? UpdatedDate { get; set; }
        public required Guid ArticleId { get; set; } 
        public required Guid UserId { get; set; } 
        public Article? Article { get; set; } 
    }
}
