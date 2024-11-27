using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class ArticleTagRepository : IArticleTagRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ArticleTagRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ArticleTags>> AddRangeAsync(List<ArticleTags> articleTags)
        {
            await dbContext.ArticleTags.AddRangeAsync(articleTags);
            await dbContext.SaveChangesAsync();

            return articleTags;
        }

        public async Task<ArticleTags> DeleteAsync(ArticleTags id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticleTags>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

    
    }
}
