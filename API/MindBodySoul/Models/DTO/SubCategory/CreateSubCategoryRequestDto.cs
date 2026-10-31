namespace MindBodySoul.Models.DTO.SubCategory
{
    public class CreateSubCategoryRequestDto
    {
        public required string Name { get; set; }
        public required Guid CategoryId { get; set; }
        public required string UrlHandle { get; set; }
        public required string Icon { get; set; }

    }
}
