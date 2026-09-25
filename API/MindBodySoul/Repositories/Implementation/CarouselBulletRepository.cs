using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class CarouselBulletRepository : ICarouselBulletRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CarouselBulletRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CarouselBullet>> AddRangeAsync(List<CarouselBullet> bullets)
        {
            await dbContext.CarouselBullets.AddRangeAsync(bullets);
            await dbContext.SaveChangesAsync();

            return bullets;
        }
    }
}
