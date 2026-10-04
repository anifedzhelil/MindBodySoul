using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface IArticleTagsRepository
    {
        Task<List<ArticleTags>> AddRangeAsync(List<ArticleTags> articleTags);
        Task<ArticleTags?> DeleteAsync(Guid articleId, Guid tagId);
        Task<List<ArticleTags>> DeleteRangeAsync(Guid articleId);
        Task<List<Guid>> GetTagIdsAsync(Guid articleId);
    }
}
