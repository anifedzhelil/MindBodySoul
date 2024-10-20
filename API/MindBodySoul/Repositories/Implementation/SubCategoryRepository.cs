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
            return await dbContext.SubCategories.ToListAsync();
        }

        public async Task<SubCategory?> GetById(Guid Id)
        {
            return await dbContext.SubCategories.FirstOrDefaultAsync(c => c.Id == Id);
        }
        public async Task<SubCategory?> UpdateAsync(SubCategory category)
        {
            var existingSubCategory = await dbContext.SubCategories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (existingSubCategory != null)
            {
                dbContext.Entry(existingSubCategory).CurrentValues.SetValues(category);
                await dbContext.SaveChangesAsync();
                return category;
            }

            return null;
        }

        public async Task<SubCategory?> DeleteAsync(Guid id)
        {
            var existingSubCategory = await dbContext.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingSubCategory is null)
            {
                return null;
            }

            dbContext.SubCategories.Remove(existingSubCategory);
            await dbContext.SaveChangesAsync();

            return existingSubCategory;
        }
    }
}
