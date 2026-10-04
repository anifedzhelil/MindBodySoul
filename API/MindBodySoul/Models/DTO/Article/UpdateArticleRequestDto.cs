using MindBodySoul.Validation;
using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Models.DTO.Article
{
    public class UpdateArticleRequestDto
    {
        [Required]
        [MaxLength(150)]
        public required string Title { get; set; }

        [Required]
        [MinTextLength(150)]
        public required string Content { get; set; }

        [NotEmptyGuid]
        public required Guid SubCategoryId { get; set; }

        [NotEmptyGuid]
        public required Guid UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public required string ImageUrl { get; set; }
        public List<Guid>? DeletedTags { get; set; }
        public List<Guid>? TagsIDs { get; set; }
    }
}
