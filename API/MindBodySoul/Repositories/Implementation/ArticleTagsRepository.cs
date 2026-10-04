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
         

        public async Task<ArticleTags?> DeleteAsync(Guid articleId, Guid tagId)
        {
            var articleTag = await dbContext.ArticleTags
              .Where(at => at.ArticleId == articleId && at.TagId == tagId)
              .FirstOrDefaultAsync();

            if(articleTag== null)
                return null;

            dbContext.ArticleTags.Remove(articleTag);

            await dbContext.SaveChangesAsync();

            return articleTag;
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

        public async Task<List<Guid>> GetTagIdsAsync(Guid articleId)
        {
            var tagIds = await dbContext.ArticleTags
                .Where(at => at.ArticleId == articleId)
                .AsNoTracking()
                .Select(at => at.TagId)
                .ToListAsync();

            return tagIds;
        }
    }
}
