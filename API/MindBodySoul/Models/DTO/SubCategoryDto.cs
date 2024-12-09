namespace MindBodySoul.Models.Domain
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public required string Name { get; set; }
        public string? UrlHandle { get; set; }
        public string? Icon { get; set; }
        public Category? Category { get; set; }
    }
}
