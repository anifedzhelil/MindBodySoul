using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class ArticleVisitsRepository : IArticleVisitsRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ArticleVisitsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ArticleVisit> AddAsync(ArticleVisit articleVisits)
        {
            await dbContext.ArticleVisits.AddAsync(articleVisits);
            await dbContext.SaveChangesAsync();

            return articleVisits;
        }

        public async Task<ArticleVisit?> DeleteAsync(Guid articleId)
        {
            var articlVisit = await dbContext.ArticleVisits
              .Where(at => at.ArticleId == articleId)
              .FirstOrDefaultAsync();

            if(articlVisit is null)
            {
                return null;
            }

            dbContext.ArticleVisits.Remove(articlVisit);

            await dbContext.SaveChangesAsync();

            return articlVisit;
        }

        public async Task<ArticleVisit?> GetAsync(Guid articleId, Guid userId)
        {
            var exestingArticleVisit = await dbContext.ArticleVisits
               .FirstOrDefaultAsync(x => x.ArticleId == articleId && x.UserId == userId);

            if(exestingArticleVisit is null)
            {
                return null;
            }

            return exestingArticleVisit;
        }

    }
}
