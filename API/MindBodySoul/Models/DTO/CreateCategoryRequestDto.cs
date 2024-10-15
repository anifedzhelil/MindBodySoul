namespace MindBodySoul.Models.DTO
{
    public class CreateCategoryRequestDto
    {
        public required string Name { get; set; }
        public required string UrlHandle { get; set; }
        public required string Image { get; set; }

    }
}
