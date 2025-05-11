using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface IArticleVisitsRepository
    {
        Task<ArticleVisit> AddAsync(ArticleVisit articleVisits);

        Task<ArticleVisit?> GetAsync(Guid articleId, Guid userId);

        Task<ArticleVisit?> DeleteAsync(Guid articleId);

    }
}
