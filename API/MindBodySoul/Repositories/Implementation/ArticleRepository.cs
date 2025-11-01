using Microsoft.AspNetCore.Http.HttpResults;
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
            var exestingArticle = await dbContext.Articles
              .FirstOrDefaultAsync(x => x.Id == id);

            if (exestingArticle is null)
            {
                return null;
            }

            dbContext.Articles.Remove(exestingArticle);
            await dbContext.SaveChangesAsync();

            return exestingArticle;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return await dbContext.Articles
                    .Where(a => a.Title.ToLower().Contains(search.ToLower()) ||
                                 a.Content.ToLower().Contains(search.ToLower())
                    ).ToListAsync();

            }
            return await dbContext.Articles.ToListAsync();
        }

        public async Task<IEnumerable<Article>?> GetAllByCategoryAsync(Guid categoryId)
        {

            var articles = await dbContext.Articles
                .Include(a => a.SubCategory)
                .Where(a => a.SubCategory != null && a.SubCategory.CategoryId == categoryId).ToListAsync();
           
            return articles;
        }

        public async Task<IEnumerable<Article>?> GetAllBySubategoryAsync(Guid subCategoryId)
        {
            var articles = await dbContext.Articles
                .Where(a => a.SubCategoryId == subCategoryId).ToListAsync();

            if (articles == null)
            {
                return null;
            }

            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllByTagAsync(Guid tagId)
        {
            var articles = await dbContext.Articles
                .Include(a => a.ArticleTags)

                .Where(a => a.ArticleTags != null && a.ArticleTags.Any(at => at.TagId == tagId))
                .ToListAsync();

            return articles;
        }

        public async Task<Article?> GetById(Guid id)
        {
            return await dbContext.Articles
                .Include(a => a.SubCategory)
                        .ThenInclude(sc => sc.Category)
                .Include(a => a.ArticleTags)
                            .ThenInclude(at => at.Tag)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Article?> UpdateAsync(Article article)
        {
            var existingArticle = await dbContext.Articles.FirstOrDefaultAsync(x => x.Id == article.Id);
            if (existingArticle != null)
            {
                dbContext.Entry(existingArticle).CurrentValues.SetValues(article);
                await dbContext.SaveChangesAsync();
                return article;
            }

            return null;
        }
    }
}
