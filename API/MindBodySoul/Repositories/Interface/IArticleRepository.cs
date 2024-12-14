using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface IArticleRepository
    {
        Task<Article> CreateAsync(Article article);
        Task<Article> UpdateAsync(Article article);
        Task<Article?> DeleteAsync(Guid id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetById(Guid id);
    }
}
