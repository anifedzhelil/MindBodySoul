namespace MindBodySoul.Models.Domain
{
    public class Carousel
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;
        public required int Order { get; set; }
        public int PaddingSide { get; set; } = 60;
        public int PaddingTop { get; set; } = 100;
        public int DescriptionSize { get; set; } = 14;
        public int BulletsSize { get; set; } = 14;
        public int NotePaddingTop { get; set; } = 15;
        public int NoteSize { get; set; } = 14;
        public string Description { get; set; } = "";
        public string Note { get; set; } = "";

        public List<CarouselBullet> Bullets { get; set; } = new();
    }
}
