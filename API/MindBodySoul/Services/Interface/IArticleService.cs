using MindBodySoul.Models.DTO.Article;

namespace MindBodySoul.Services.Interface
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetArticlesAsync(string? search = null);
        Task<IEnumerable<ArticleDto>> GetArticlesByTagAsync(Guid byTag);
        Task<IEnumerable<ArticleDto>> GetArticlesBySubCategoryAsync(Guid subCategoryId);
        Task<IEnumerable<ArticleDto>> GetArticlesByCategoryAsync(Guid categoryId);
        Task<ArticleDetailsDto?> GetArticleByIdAsync(Guid articleId);
        Task<bool> DeleteArticleAsync(Guid articleId);
        Task CreateArticleAsync(CreateArticleRequestDto articleRequest);
        Task<bool> UpdateArticleAsync(Guid articleId, UpdateArticleRequestDto articleRequest);
        Task<IEnumerable<LatestArticleDto>> GetLatestArticlesByLimit(int limit);

    }
}
