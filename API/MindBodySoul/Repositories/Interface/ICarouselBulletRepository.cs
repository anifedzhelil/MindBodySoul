using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ICarouselBulletRepository
    {
        Task<List<CarouselBullet>> AddRangeAsync(List<CarouselBullet> bullets);

    }
}
