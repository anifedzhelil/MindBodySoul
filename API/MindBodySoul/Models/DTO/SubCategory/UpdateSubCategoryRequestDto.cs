using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Models.DTO.SubCategory
{
    public class UpdateSubCategoryRequestDto
    {
        public required Guid CategoryId { get; set; }
        public required string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-z0-9]+(-[a-z0-9]+)*$", ErrorMessage = "Само малки латински букви, цифри и тирета!")]
        public required string UrlHandle { get; set; }
        public required string Icon { get; set; }
    }
}
