using Microsoft.AspNetCore.Identity;

namespace MindBodySoul.Models.Domain
{
    public class Comment
    {
        public Guid Id { get; set; } 
        public required string Content { get; set; } 
        public  DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
        public DateTime? UpdatedDate { get; set; }
        public  Guid ArticleId { get; set; } 
        public  Guid UserId { get; set; } 
        public Article? Article { get; set; } 
    }
}
