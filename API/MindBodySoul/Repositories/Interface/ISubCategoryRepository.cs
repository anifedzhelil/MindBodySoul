using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ISubCategoryRepository
    {
        Task<SubCategory> CreateAsync(SubCategory subCategory);
        Task<SubCategory?> UpdateAsync(SubCategory subCategory);
        Task<SubCategory> DeleteAsync(Guid id);       
        Task<IEnumerable<SubCategory>> GetAllAsync();
        Task<SubCategory?> GetById(Guid id);
        Task<IEnumerable<SubCategory>> GetByCategoryId(Guid categoryId);
        Task<IEnumerable<SubCategory>> GetAllWithPublishedArticlesAsync();
        Task<bool> UrlHandleExistsAsync(Guid? id, string url);
        Task<bool> NameExistsAsync(Guid? id, string name);
    }
}
