using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ArticleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Article> CreateAsync(Article article)
        {
            await dbContext.Articles.AddAsync(article);
            await dbContext.SaveChangesAsync();
            return article;
        }

        public async Task<Article?> DeleteAsync(Guid id)
        {
            var existingArticle = await dbContext.Articles
              .FirstOrDefaultAsync(x => x.Id == id);

            if (existingArticle is null)
            {
                return null;
            }

            dbContext.Articles.Remove(existingArticle);
            await dbContext.SaveChangesAsync();

            return existingArticle;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await dbContext.Articles
                    .Where(a => a.Title.ToLower().Contains(search.ToLower()) ||
                                 a.Content.ToLower().Contains(search.ToLower()))
                    .OrderByDescending(a => a.CreatedDate)
                    .AsNoTracking()
                    .ToListAsync();

            }
            return await dbContext.Articles
                .OrderByDescending(a => a.CreatedDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetAllByCategoryAsync(Guid categoryId)
        {
            var articles = await dbContext.Articles
                .Where(a => a.SubCategory != null && a.SubCategory.CategoryId == categoryId)
                .OrderByDescending(a => a.CreatedDate)
                .AsNoTracking()
                .ToListAsync();

            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllBySubCategoryAsync(Guid subCategoryId)
        {
            var articles = await dbContext.Articles
                .Where(a => a.SubCategoryId == subCategoryId)
                .OrderByDescending(a => a.CreatedDate)
                .AsNoTracking()
                .ToListAsync();

            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllByTagAsync(Guid tagId)
        {
            var articles = await dbContext.Articles
                .Where(a => a.ArticleTags != null && a.ArticleTags.Any(at => at.TagId == tagId))
                .OrderByDescending(a => a.CreatedDate)
                .AsNoTracking()
                .ToListAsync();

            return articles;
        }

        public async Task<Article?> GetByIdAsync(Guid id)
        {
            return await dbContext.Articles
                .Include(a => a.SubCategory)
                        .ThenInclude(sc => sc!.Category)
                .Include(a => a.ArticleTags!)
                            .ThenInclude(at => at.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Article?> GetByIdForUpdateAsync(Guid id)
        {
            return await dbContext.Articles
                .Include(a => a.ArticleTags)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Article>> GetLatestArticlesAsync(int limit)
        {
            var articles = await dbContext.Articles
                .OrderByDescending(a => a.CreatedDate)
                .Take(limit)
                .AsNoTracking()
                .ToListAsync();

            return articles;
        }

        public async Task<Article?> UpdateAsync(Article article)
        {
            var existingArticle = await dbContext.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);
            if (existingArticle == null) return null;

            existingArticle.SubCategoryId = article.SubCategoryId;
            existingArticle.ImageUrl = article.ImageUrl;
            existingArticle.Title = article.Title;
            existingArticle.Content = article.Content;
            existingArticle.UpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return existingArticle;
        }

        // Increase the counters directly in the database, no entity loading needed
        public async Task IncrementVisitCountsAsync(Guid articleId, bool isUniqueVisit)
        {
            var query = dbContext.Articles.Where(a => a.Id == articleId);

            if (isUniqueVisit)
            {
                await query.ExecuteUpdateAsync(s => s
                    .SetProperty(a => a.TotalVisitCount, a => a.TotalVisitCount + 1)
                    .SetProperty(a => a.UniqueVisitCount, a => a.UniqueVisitCount + 1));
            }
            else
            {
                await query.ExecuteUpdateAsync(s => s
                    .SetProperty(a => a.TotalVisitCount, a => a.TotalVisitCount + 1));
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await dbContext.Articles.Where(a => a.Id == id).AnyAsync();
        }
    }
}
