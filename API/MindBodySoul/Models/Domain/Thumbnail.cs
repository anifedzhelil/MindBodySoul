namespace MindBodySoul.Models.Domain
{
    public class Thumbnail
    {
        public Guid Id { get; set; }
        public Guid PostId {get; set;}
        public Post Post { get; set; } = null!;
        public required string Image { get; set; }
        public required string Category { get; set; }
        public double Zoom { get; set; } = 1.0;
        public  int OffsetX { get; set; } = 0;
        public int OffsetY { get; set; } = 0;
        public required string Title { get; set; }
        public int TitleFontSize { get; set; } = 28;
        public int TitleTop { get; set; } = 20;

    }
}
