using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);       
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetById(Guid id);
        Task<bool> UrlHandleExistsAsync(Guid? id, string url);
        Task<bool> NameExistsAsync(Guid? id, string url);
        Task SaveChangesAsync();

    }
}
