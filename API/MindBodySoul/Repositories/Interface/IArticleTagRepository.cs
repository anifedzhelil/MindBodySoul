using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface IArticleTagRepository
    {
        Task<List<ArticleTags>> AddRangeAsync(List<ArticleTags> articleTags);
        Task<ArticleTags> DeleteAsync(ArticleTags id);
        Task<IEnumerable<ArticleTags>> GetAllAsync();
    }
}
