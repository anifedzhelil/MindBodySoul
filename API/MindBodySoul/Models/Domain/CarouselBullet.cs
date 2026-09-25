namespace MindBodySoul.Models.Domain
{
    public class CarouselBullet
    {
        public int Id { get; set; }

        public string Header { get; set; } = "";
        public string Text { get; set; } = "";
        public int Order { get; set; }
        public Guid CarouselDraftId { get; set; }
        public Carousel CarouselDraft { get; set; } = null!;

    }
}
