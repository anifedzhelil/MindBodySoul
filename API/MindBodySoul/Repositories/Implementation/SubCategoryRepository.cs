﻿using Microsoft.EntityFrameworkCore;
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
                 .Select(s => new SubCategory
                 {
                     Id = s.Id,
                     Name = s.Name,
                     CategoryId = s.CategoryId,
                     Icon =  s.Icon,
                     UrlHandle = s.UrlHandle,
                     Category = s.Category,
                 })
       .ToListAsync();

            return subCategories;
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
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();

            return subCategories;
        }
    }
}
