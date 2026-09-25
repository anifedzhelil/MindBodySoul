namespace MindBodySoul.Models.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public  required string PostName { get; set; }
        public Thumbnail? Thumbnail { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPublished { get; set; } = false;
        public ICollection<Carousel>? CarouselDrafts { get; set; } = new List<Carousel>();
    }
}
