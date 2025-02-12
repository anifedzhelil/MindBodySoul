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

        public async Task<ArticleVisit?> GetAsync(Guid articleId)
        {
            var exestingArticleVisit = await dbContext.ArticleVisits
               .FirstOrDefaultAsync(x => x.ArticleId == articleId);

            if(exestingArticleVisit is null)
            {
                return null;
            }

            return exestingArticleVisit;
        }
    }
}
