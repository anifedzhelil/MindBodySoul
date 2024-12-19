namespace MindBodySoul.Models.DTO
{
    public class UpdateArticleRequestDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Guid SubCategoryId { get; set; }
        public required Guid UserId { get; set; }
        public required string ImageUrl { get; set; }
        public required DateTime UpdatedDate { get; set; }
        public List<Guid>? DeletedTags { get; set; }
        public List<Guid>? TagsIDs { get; set; }
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
