using MindBodySoul.Models.Domain;

namespace MindBodySoul.Models.DTO
{
    public class ArticleTagsDto
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
    }
}
