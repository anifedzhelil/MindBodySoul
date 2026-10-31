using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SubCategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<SubCategory> CreateAsync(SubCategory subCategory)
        {
            object value = await dbContext.SubCategories.AddAsync(subCategory);
            await dbContext.SaveChangesAsync();
            return subCategory;
        }
        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            var subCategories = await dbContext.SubCategories.Include(x => x.Category)
                 .AsNoTracking()
                 .Include(x => x.Category)

                 .Select(s => new SubCategory
                 {
                     Id = s.Id,
                     Name = s.Name,
                     CategoryId = s.CategoryId,
                     Icon = s.Icon,
                     UrlHandle = s.UrlHandle,
                     Category = s.Category,
                 })
       .ToListAsync();

            return subCategories;
        }
        public async Task<SubCategory?> GetById(Guid Id)
        {
            return await dbContext.SubCategories
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == Id);
        }
        public async Task<SubCategory?> UpdateAsync(SubCategory subCategory)
        {
            var existingSubCategory = await dbContext.SubCategories
                .FirstOrDefaultAsync(x => x.Id == subCategory.Id);

            if (existingSubCategory is null)
            {
                return null;
            }

            existingSubCategory.Name = subCategory.Name;
            existingSubCategory.UrlHandle = subCategory.UrlHandle;
            existingSubCategory.Icon = subCategory.Icon;
            existingSubCategory.CategoryId = subCategory.CategoryId;

            await dbContext.SaveChangesAsync();

            return existingSubCategory;
        }
        public async Task<SubCategory?> DeleteAsync(Guid id)
        {
            var existingSubCategory = await dbContext.SubCategories.Include(s => s.Articles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingSubCategory is null)
            {
                return null;
            }

            if (existingSubCategory.Articles != null && existingSubCategory.Articles.Any())
            {
                return existingSubCategory;
            }


            dbContext.SubCategories.Remove(existingSubCategory);
            await dbContext.SaveChangesAsync();

            return existingSubCategory;
        }
        public async Task<IEnumerable<SubCategory>> GetByCategoryId(Guid categoryId)
        {
            var subCategories = await dbContext.SubCategories
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId
                    && x.Articles != null
                    && x.Articles.Any(a => a.IsPublished && !a.IsArchived))
                .ToListAsync();

            return subCategories;
        }
        public async Task<IEnumerable<SubCategory>> GetAllWithPublishedArticlesAsync()
        {
            var subCategories = await dbContext.SubCategories
                .AsNoTracking()
                .Where(s => s.Articles != null && s.Articles.Any(a => a.IsPublished && !a.IsArchived)).ToListAsync();

            return subCategories;
        }
        public async Task<bool> UrlHandleExistsAsync(Guid? id, string url)
        {
            return await dbContext.SubCategories
                .AnyAsync(a => a.UrlHandle == url && (id == null || a.Id != id));
        }

        public async Task<bool> NameExistsAsync(Guid? id, string name)
        {
            return await dbContext.SubCategories
                .AnyAsync(a => a.Name == name && (id == null || a.Id != id));
        }
    }
}
