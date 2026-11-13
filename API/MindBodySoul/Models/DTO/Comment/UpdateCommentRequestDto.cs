using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Models.DTO.Comment
{
    public class UpdateCommentRequestDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(1000)]
        public required string Content { get; set; }
    }
}

