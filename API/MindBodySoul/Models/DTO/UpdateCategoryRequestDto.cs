namespace CodePulse.API.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        public required string Name { get; set; }

        public required string UrlHandle { get; set; }

        public required string Image { get; set; }
    }
}
