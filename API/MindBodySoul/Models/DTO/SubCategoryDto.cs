namespace MindBodySoul.Models.Domain
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }
        public required Guid CategoryId { get; set; }
        public required string Name { get; set; }
        public required string UrlHandle { get; set; }
        public required string Icon { get; set; }
    }
}
