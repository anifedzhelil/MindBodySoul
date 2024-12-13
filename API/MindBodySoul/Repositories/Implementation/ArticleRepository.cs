using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Migrations;
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

        public Task<Article> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await dbContext.Articles.ToListAsync();
        }

        public async Task<Article?> GetById(Guid id)
        {
            return await dbContext.Articles
                .Include(a=>a.SubCategory)
                        .ThenInclude(sc => sc.Category) 
                .Include(a => a.ArticleTags)
                            .ThenInclude(at => at.Tag)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<Article> UpdateAsync(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
