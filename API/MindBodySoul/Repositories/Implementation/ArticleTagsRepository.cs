using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class ArticleTagsRepository : IArticleTagsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ArticleTagsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ArticleTags>> AddRangeAsync(List<ArticleTags> articleTags)
        {
            await dbContext.ArticleTags.AddRangeAsync(articleTags);
            await dbContext.SaveChangesAsync();

            return articleTags;
        }

        public Task<ArticleTags> DeleteAsync(ArticleTags id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleTags>> DeleteRangeAsync(Guid articleId)
        {
            var articleTags = await dbContext.ArticleTags
               .Where(at => at.ArticleId == articleId)
               .ToListAsync();

            dbContext.ArticleTags.RemoveRange(articleTags);

            await dbContext.SaveChangesAsync();

            return articleTags;
        }

        public Task<IEnumerable<ArticleTags>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    
    }
}
