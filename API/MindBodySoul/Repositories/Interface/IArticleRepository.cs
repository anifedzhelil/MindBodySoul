using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface IArticleRepository
    {
        Task<Article> CreateAsync(Article article);
        Task<Article> UpdateAsync(Article article);
        Task<Article?> DeleteAsync(Guid id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<IEnumerable<Article>?> GetAllBySubategoryAsync(Guid subCategoryId);
        Task<IEnumerable<Article>?> GetAllByCategoryAsync(Guid categoryId);
        Task<Article?> GetById(Guid id);
    }
}
