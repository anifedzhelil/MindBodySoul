using System.ComponentModel.DataAnnotations;

namespace MindBodySoul.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string UrlHandle { get; set; }
        public required string Image { get; set; }
        public ICollection<SubCategory>? SubCategories { get; set; }




    }
}
