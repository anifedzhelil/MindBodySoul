using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class CarouselRepository : ICarouselRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CarouselRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Carousel> CreateAsync(Carousel carousel)
        {
            await dbContext.Carousels.AddAsync(carousel);
            await dbContext.SaveChangesAsync();
            return carousel;
        }

        public async Task<IEnumerable<Carousel>> GetOrderedIdsByPostIdAsync(Guid postId)
        {
            var carousels = await dbContext.Carousels.Where(x => x.PostId == postId)
                .Select(c => new Carousel
                { 
                    Id = c.Id,
                    Order = c.Order
                }).ToListAsync();

            return carousels;
        }

        public async Task<Carousel?> GetByIdAsync(Guid id)
        {
            return await dbContext.Carousels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Carousel?> UpdateAsync(Carousel carousel)
        {
            var existingCarousel = await dbContext.Carousels.FirstOrDefaultAsync(x => x.Id == carousel.Id);
            if (existingCarousel == null) return null;

            existingCarousel.Description = carousel.Description;
            existingCarousel.Order = carousel.Order;
            existingCarousel.Note = carousel.Note;
            existingCarousel.NoteSize = carousel.NoteSize;
            existingCarousel.PaddingSide = carousel.PaddingSide;
            existingCarousel.PaddingTop = carousel.PaddingTop;
            existingCarousel.BulletsSize = carousel.BulletsSize;
            existingCarousel.DescriptionSize = carousel.DescriptionSize;
            existingCarousel.NotePaddingTop = carousel.NotePaddingTop;

            var incomingIds= carousel.Bullets.Select(b  => b.Id).ToHashSet();

            var bulletsToRemove = existingCarousel.Bullets
                .Where( b=> incomingIds.Contains(b.Id) )
                .ToList();


            foreach (var bullet in carousel.Bullets)
            {
                var existingBullet = existingCarousel.Bullets.FirstOrDefault(b => b.Id == bullet.Id);
                if (existingBullet != null)
                {
                    existingBullet.Header = bullet.Header;
                    existingBullet.Text = bullet.Text;
                    existingBullet.Order = bullet.Order;
                }
                else if (existingBullet == null)
                {
                    existingCarousel.Bullets.Add(bullet);
                }
            }

            await dbContext.SaveChangesAsync();
            return existingCarousel;
        }

        public async Task<Carousel?> DeleteByIdAsync(Guid id)
        {
            var existingCarousel = await dbContext.Carousels
                        .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCarousel is null)
            {
                return null;
            }

            dbContext.Carousels.Remove(existingCarousel);

            await dbContext.SaveChangesAsync();

            return existingCarousel;
        
        }
     }
}
