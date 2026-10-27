using MindBodySoul.Models.DTO.Category;
using MindBodySoul.Models.Enum;

namespace MindBodySoul.Services.Interface
{
    public interface ICategoryService
    {
        Task<OperationResult> CreateCategoryAsync(CreateCategoryRequestDto reqest);
        Task<CategoryDto?> GetCategoryAsync(Guid id);
        Task<IEnumerable<CategoryDto>> GetCategorisAsync();
        Task<OperationResult> UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request);
        Task<OperationResult> DeleteCategoryAsync(Guid id);


    }
}
