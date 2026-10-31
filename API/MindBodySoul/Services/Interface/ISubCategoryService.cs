
using MindBodySoul.Models.DTO.SubCategory;
using MindBodySoul.Models.Enum;

namespace MindBodySoul.Services.Interface
{
    public interface ISubCategoryService 
    {
        Task<OperationResult> UpdateSubCategoryAsync(Guid id, UpdateSubCategoryRequestDto request);
        Task<OperationResult> CreateSubCategoryAsync(CreateSubCategoryRequestDto request);
        Task<IEnumerable<SubCategoryDto>> GetPublishedSubCategoriesAsync();
        Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByCategoryIdAsync(Guid categoryId);
        Task<IEnumerable<SubCategoryDto>> GetSubCategoriesAsync();
        Task<SubCategoryDto?> GetSubCategoryAsync(Guid id);
        Task<OperationResult> DeleteSubCategoryAsync(Guid id);
    }
}
