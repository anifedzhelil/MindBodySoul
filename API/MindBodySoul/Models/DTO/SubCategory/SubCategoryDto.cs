using DomainCategory = MindBodySoul.Models.Domain.Category;

namespace MindBodySoul.Models.DTO.SubCategory
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public required string Name { get; set; }
        public string? UrlHandle { get; set; }
        public string? Icon { get; set; }
        public DomainCategory? Category { get; set; }
    }
}
