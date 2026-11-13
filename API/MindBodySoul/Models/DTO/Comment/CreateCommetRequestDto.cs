using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Models.DTO.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MaxLength(1000)]
        public required string Content { get; set; }
        public required Guid ArticleId { get; set; }

    }
}
