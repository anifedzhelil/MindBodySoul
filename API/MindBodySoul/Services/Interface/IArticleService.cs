using MindBodySoul.Models.DTO.Article;
using MindBodySoul.Models.Enum;

namespace MindBodySoul.Services.Interface
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetArticlesAsync(string? search = null);
        Task<IEnumerable<ArticleDto>> GetArticlesByTagAsync(Guid byTag);
        Task<IEnumerable<ArticleDto>> GetArticlesBySubCategoryAsync(Guid subCategoryId);
        Task<IEnumerable<ArticleDto>> GetArticlesByCategoryAsync(Guid categoryId);
        Task<ArticleDetailsDto?> GetArticleByIdAsync(Guid articleId);
        Task<OperationResult> DeleteArticleAsync(Guid articleId, Guid userId);
        Task CreateArticleAsync(CreateArticleRequestDto articleRequest, Guid userId);
        Task<OperationResult> UpdateArticleAsync(Guid articleId, UpdateArticleRequestDto articleRequest, Guid userId);
        Task<IEnumerable<LatestArticleDto>> GetLatestArticlesByLimit(int limit);

    }
}
