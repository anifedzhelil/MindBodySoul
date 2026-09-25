using Microsoft.EntityFrameworkCore;
using MindBodySoul.Models.Domain;

namespace MindBodySoul.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTags> ArticleTags { get; set; }
        public DbSet<ArticleVisit> ArticleVisits { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<CarouselBullet> CarouselBullets { get; set; }

    }
}
