using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface IArticleTagsRepository
    {
        Task<List<ArticleTags>> AddRangeAsync(List<ArticleTags> articleTags);
        Task<ArticleTags> DeleteAsync(ArticleTags id);
        Task<ArticleTags> DeleteAsync(Guid articleId, Guid tagId);
        Task<IEnumerable<ArticleTags>> GetAllAsync();
        Task<List<ArticleTags>> DeleteRangeAsync(Guid articleId);

    }
}
