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
    }
}
