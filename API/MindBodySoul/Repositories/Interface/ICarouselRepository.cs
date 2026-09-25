using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ICarouselRepository
    {
        Task<Carousel> CreateAsync(Carousel carousel);

        Task<Carousel?> UpdateAsync(Carousel carousel);

        Task<IEnumerable<Carousel>> GetOrderedIdsByPostIdAsync(Guid postId);

        Task<Carousel?> GetByIdAsync(Guid id);

        Task<Carousel?> DeleteByIdAsync(Guid id);
    }
}
