using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ISubCategoryRepository
    {
        Task<SubCategory> CreateAsync(SubCategory subCategory);
        Task<SubCategory> UpdateAsync(SubCategory subCategory);
        Task<SubCategory> DeleteAsync(Guid id);       

        Task<IEnumerable<SubCategory>> GetAllAsync();

        Task<SubCategory?> GetById(Guid id);

    }
}
