using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.Enum;
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

        private IQueryable<Article> GetPublishedArticlesQuery()
        {
            return dbContext.Articles
                .Where(a => a.IsPublished && !a.IsArchived)
                .OrderByDescending(a => a.PublishedDate)
                .AsNoTracking();
        }
        public async Task<Article> CreateAsync(Article article)
        {
            await dbContext.Articles.AddAsync(article);
            await dbContext.SaveChangesAsync();
            return article;
        }

        public async Task DeleteAsync(Article article)
        {
            dbContext.Articles.Remove(article);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> GetAllAsync(string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower();
                return await GetPublishedArticlesQuery()
                    .Where(a => a.Title.ToLower().Contains(lowerSearch) ||
                                 a.ContentText.ToLower().Contains(lowerSearch))
                    .ToListAsync();

            }
            return await GetPublishedArticlesQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetAllByCategoryAsync(Guid categoryId)
        {
            var articles = await GetPublishedArticlesQuery()
                .Where(a => a.SubCategory != null && a.SubCategory.CategoryId == categoryId)
                .ToListAsync();

            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllBySubCategoryAsync(Guid subCategoryId)
        {
            var articles = await GetPublishedArticlesQuery()
                .Where(a => a.SubCategoryId == subCategoryId)
                .ToListAsync();

            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllByTagAsync(Guid tagId)
        {
            var articles = await GetPublishedArticlesQuery()
                .Where(a => a.ArticleTags != null && a.ArticleTags.Any(at => at.TagId == tagId))
                .ToListAsync();

            return articles;
        }

        public async Task<IEnumerable<Article>> GetFilteredForAdminAsync(ArticleStatusFilter statusFilter)
        {
            IQueryable<Article> query = dbContext.Articles;

            switch (statusFilter)
            {
                case ArticleStatusFilter.All:
                    // No extra condition, return everything
                    break;
                case ArticleStatusFilter.Draft:
                    query = query.Where(a => !a.IsPublished && !a.IsArchived);
                    break;
                case ArticleStatusFilter.Published:
                    query = query.Where(a => a.IsPublished && !a.IsArchived);
                    break;
                case ArticleStatusFilter.Archived:
                    query = query.Where(a => a.IsArchived);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(statusFilter), statusFilter, null);
            }

            return await query
                    .OrderByDescending(a => a.CreatedDate)
                    .Select(a => new Article
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Slug = a.Slug,
                        ImageUrl = a.ImageUrl,
                        SubCategoryId = a.SubCategoryId,
                        UserId = a.UserId,
                        // Heavy fields are not read from the database, they only satisfy 'required'
                        Content = string.Empty,
                        ContentText = string.Empty,
                        IsPublished = a.IsPublished,
                        IsArchived = a.IsArchived,
                        CreatedDate = a.CreatedDate,
                        PublishedDate = a.PublishedDate,
                        UpdatedDate = a.UpdatedDate,
                        TotalVisitCount = a.TotalVisitCount,
                        UniqueVisitCount = a.UniqueVisitCount
                    })
                    .ToListAsync();
        }

        public async Task<Article?> GetPublishedByIdAsync(Guid id)
        {
            return await dbContext.Articles
                .Include(a => a.SubCategory)
                        .ThenInclude(sc => sc!.Category)
                .Include(a => a.ArticleTags!)
                            .ThenInclude(at => at.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && a.IsPublished && !a.IsArchived);
        }

        public async Task<Article?> GetByIdForEditAsync(Guid id)
        {
            return await dbContext.Articles
                .Include(a => a.SubCategory)
                        .ThenInclude(sc => sc!.Category)
                .Include(a => a.ArticleTags!)
                            .ThenInclude(at => at.Tag)
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
            return await GetPublishedArticlesQuery()
                .Take(limit)
                .ToListAsync();
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

        public async Task SetArchiveStatusAsync(Guid articleId, bool isArchived)
        {
            var article = await dbContext.Articles.FindAsync(articleId);
            if (article != null)
            {
                article.IsArchived = isArchived;
                await dbContext.SaveChangesAsync();
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

        public async Task<bool> SlugExistsAsync(Guid? id, string slug)
        {
            return await dbContext.Articles
                .AnyAsync(a => a.Slug == slug && (id == null || a.Id != id));
        }
    }
}
