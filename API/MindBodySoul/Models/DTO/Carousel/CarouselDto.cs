using MindBodySoul.Models.Domain;

namespace MindBodySoul.Models.DTO.CarouselDraft
{
    public class CarouselDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }

        public int Order { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int PaddingSide { get; set; }
        public int PaddingTop { get; set; }
        public int DescriptionSize { get; set; }
        public int BulletsSize { get; set; }
        public int NotePaddingTop { get; set; }
        public int NoteSize { get; set; }
        public string Description { get; set; } = "";
        public string Note { get; set; } = "";

        public List<CarouselBullet> Bullets { get; set; } = new();
    }
}
