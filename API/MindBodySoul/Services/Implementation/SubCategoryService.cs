using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO.SubCategory;
using MindBodySoul.Models.Enum;
using MindBodySoul.Repositories.Implementation;
using MindBodySoul.Repositories.Interface;
using MindBodySoul.Services.Interface;

namespace MindBodySoul.Services.Implementation
{
    public class SubCategoryService: ISubCategoryService
    { 
        private ISubCategoryRepository subCategoryRepository;
        public SubCategoryService(ISubCategoryRepository subCategoryRepository) {
            this.subCategoryRepository = subCategoryRepository;
        }

        private SubCategoryDto GetSubCategory(SubCategory subCategory)
        {
            var subCategoryDto = new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                UrlHandle = subCategory.UrlHandle,
                Icon = subCategory.Icon,
                CategoryId = subCategory.CategoryId,
                Category = subCategory.Category
            };

            return subCategoryDto;
        }
        public async Task<OperationResult> CreateSubCategoryAsync(CreateSubCategoryRequestDto request)
        {
            var urlHandleExists = await subCategoryRepository.UrlHandleExistsAsync(null, request.UrlHandle);
            if (urlHandleExists)
            {
                return OperationResult.UrlHandleConflict;
            }

            var nameExists = await subCategoryRepository.NameExistsAsync(null, request.Name);
            if (nameExists)
            {
                return OperationResult.NameConflict;
            }

            var subCategory = new SubCategory
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
                Icon = request.Icon,
                CategoryId = request.CategoryId
            };

            await subCategoryRepository.CreateAsync(subCategory);

            return OperationResult.Success;
        }

        public async Task<OperationResult> UpdateSubCategoryAsync(Guid id, UpdateSubCategoryRequestDto request)
        {
            var isUrlHandleTaken = await subCategoryRepository.UrlHandleExistsAsync(id, request.UrlHandle);
            if (isUrlHandleTaken)
            {
                return OperationResult.UrlHandleConflict;
            }

            var isNameTaken = await subCategoryRepository.NameExistsAsync(id, request.Name);
            if (isNameTaken)
            {
                return OperationResult.NameConflict;
            }

            var subCategory = new SubCategory
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
                Icon = request.Icon,
                CategoryId = request.CategoryId
            };

            // Repository returns null if the subcategory does not exist.
            var updatedSubCategory = await subCategoryRepository.UpdateAsync(subCategory);

            if (updatedSubCategory == null)
            {
                return OperationResult.NotFound;
            }

            return OperationResult.Success;
        }

        public async Task<SubCategoryDto?> GetSubCategoryAsync(Guid id)
        {
            var subCategory = await subCategoryRepository.GetById(id);

            if (subCategory == null)
            {
                return null;
            }

            return GetSubCategory(subCategory);
        }

        public async Task<IEnumerable<SubCategoryDto>> GetSubCategoriesAsync()
        {
            var subCategories = await subCategoryRepository.GetAllAsync();

            return subCategories.Select(GetSubCategory).ToList();
        }

        public async Task<IEnumerable<SubCategoryDto>> GetPublishedSubCategoriesAsync()
        {
            var subCategories = await subCategoryRepository.GetAllWithPublishedArticlesAsync();

            return subCategories.Select(GetSubCategory).ToList();
        }
        public async Task<IEnumerable<SubCategoryDto>> GetSubCategoriesByCategoryIdAsync(Guid categoryId)
        {
            var subCategories = await subCategoryRepository.GetByCategoryId(categoryId);

            return subCategories.Select(GetSubCategory).ToList();
        }
     
        public async Task<OperationResult> DeleteSubCategoryAsync(Guid id)
        {
            var existingSubCategory = await subCategoryRepository.DeleteAsync(id);

            if (existingSubCategory is null)
            {
                return OperationResult.NotFound;
            }

            if (existingSubCategory.Articles != null && existingSubCategory.Articles.Any())
            {
                return OperationResult.BadRequest;
            }

            return OperationResult.Success;
        }

    }
}
