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
        Task<IEnumerable<AdminArticleListItemDto>> GetFilteredForAdmin(ArticleStatusFilter statusFilter);
        Task<ArticleDetailsDto?> GetArticleByIdAsync(Guid articleId);
        Task<ArticleEditDto?> GetArticleForEditAsync(Guid articleId);
        Task<ArticleDetailsDto?> GetArticleForPreviewAsync(Guid articleId);
        Task<OperationResult> DeleteArticleAsync(Guid articleId);
        Task<OperationResult> CreateArticleAsync(CreateArticleRequestDto articleRequest, Guid userId);
        Task<OperationResult> UpdateArticleAsync(Guid articleId, UpdateArticleRequestDto articleRequest);
        Task<IEnumerable<LatestArticleDto>> GetLatestArticlesByLimit(int limit);
        Task<OperationResult> UpdateArchiveStatusAsync(Guid articleId, bool isArchived);

    }
}
