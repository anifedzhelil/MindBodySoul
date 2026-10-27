using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid Id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == Id);
        }
        /*
        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (existingCategory != null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }
        */
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        // Deletes the category only if it has no subcategories.
        // Returns null if the category does not exist.
        // If subcategories exist, returns the category unchanged and does not delete it.

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dbContext.Categories
                .Include(c=>c.SubCategories)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null)
            {
                return null;
            }

            if (existingCategory.SubCategories != null && existingCategory.SubCategories.Any())
            {
                return existingCategory; 
            }

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<bool> UrlHandleExistsAsync(Guid? id, string url)
        {
            return await dbContext.Categories
                .AnyAsync(a => a.UrlHandle == url && (id == null || a.Id != id));
        }

        public async Task<bool> NameExistsAsync(Guid? id, string name)
        {
            return await dbContext.Categories
                .AnyAsync(a=>a.Name == name && (id == null || a.Id != id));
        }
    }
}
