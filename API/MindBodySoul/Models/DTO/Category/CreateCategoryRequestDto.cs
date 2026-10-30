using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Models.DTO.Category
{
    public class CreateCategoryRequestDto
    {
        public required string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-z0-9]+(-[a-z0-9]+)*$", ErrorMessage = "Само малки латински букви, цифри и тирета!")]
        public required string UrlHandle { get; set; }
        public required string Image { get; set; }

    }
}
