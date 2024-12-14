using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface IArticleTagsRepository
    {
        Task<List<ArticleTags>> AddRangeAsync(List<ArticleTags> articleTags);
        Task<ArticleTags> DeleteAsync(ArticleTags id);
        Task<IEnumerable<ArticleTags>> GetAllAsync();
        Task<List<ArticleTags>> DeleteRangeAsync(Guid articleId);

    }
}
