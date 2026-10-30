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
        [RegularExpression(@"^[a-z0-9]+(-[a-z0-9]+)*$", ErrorMessage = "Само малки латински букви, цифри и тирета!")]
        [MaxLength(200)]
        public required string Slug { get; set; }

     
        [Required]
        [MinTextLength(150)]
        public required string Content { get; set; }

        [NotEmptyGuid]
        public required Guid SubCategoryId { get; set; }

        [Required]
        [MaxLength(500)]
        public required string ImageUrl { get; set; }
        public bool IsPublished { get; set; }
        public List<Guid>? DeletedTags { get; set; }
        public List<Guid>? TagsIDs { get; set; }
    }
}
