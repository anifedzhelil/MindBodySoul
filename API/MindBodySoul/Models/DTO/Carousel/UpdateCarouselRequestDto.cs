namespace MindBodySoul.Models.DTO.CarouselDraft
{
    public class UpdateCarouselRequestDto
    {
        public string Description { get; set; }
        public required int Order { get; set; }
        public string Note { get; set; }
        public int NoteSize { get; set; }
        public int PaddingSide { get; set; }
        public int PaddingTop { get; set; }
        public int BulletsSize { get; set; }
        public int DescriptionSize { get; set; }
        public int NotePaddingTop { get; set; }
    }
}