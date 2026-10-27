namespace MindBodySoul.Models.DTO.Category
{
    public class UpdateCategoryRequestDto
    {
        public required string Name { get; set; }

        public required string UrlHandle { get; set; }

        public required string Image { get; set; }
    }
}
