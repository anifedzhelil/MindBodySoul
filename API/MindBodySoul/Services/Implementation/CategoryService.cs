using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO.Category;
using MindBodySoul.Models.Enum;
using MindBodySoul.Repositories.Implementation;
using MindBodySoul.Repositories.Interface;
using MindBodySoul.Services.Interface;

namespace MindBodySoul.Services.Implementation
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

       
        public async Task<OperationResult> CreateCategoryAsync(CreateCategoryRequestDto reqest)
        {
            var isCategoryExists = await categoryRepository.UrlHandleExistsAsync(null, reqest.UrlHandle);
            if (isCategoryExists)
            {
                return OperationResult.UrlHandleConflict;
            }

            isCategoryExists = await categoryRepository.NameExistsAsync(null, reqest.Name);
            if (isCategoryExists)
            {
                return OperationResult.NameConflict;
            }

            var category = new Category
            {
                Name = reqest.Name,
                UrlHandle = reqest.UrlHandle,
                Image = reqest.Image
            };

            await categoryRepository.CreateAsync(category);

            return OperationResult.Success;
        }

        public async Task<CategoryDto?> GetCategoryAsync( Guid id)
        {
            var category = await categoryRepository.GetById(id);

            if (category == null)
            {
                return null;
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
                Image = category.Image

            };

            return response;
        }

        public async Task<IEnumerable<CategoryDto>>  GetCategorisAsync()
        {
            var categories = await categoryRepository.GetAllAsync();

            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                    Image = category.Image

                });
            }

            return response;
        }

        public async Task<OperationResult> UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request)
        {
            var existingCategory = await categoryRepository.GetById(id);

            if (existingCategory == null)
            {
                return OperationResult.NotFound;
            }

            var isCategoryExists = await categoryRepository.UrlHandleExistsAsync(id, request.UrlHandle);
            if (isCategoryExists)
            {
                return OperationResult.UrlHandleConflict;
            }

            isCategoryExists = await categoryRepository.NameExistsAsync(id, request.Name);
            if (isCategoryExists)
            {
                return OperationResult.NameConflict;
            }

            existingCategory.Name = request.Name;
            existingCategory.UrlHandle = request.UrlHandle;
            existingCategory.Image = request.Image;

            await categoryRepository.SaveChangesAsync();

            return OperationResult.Success;
        }

        // Returns NotFound if the category does not exist.
        // Returns BadRequest if the category has subcategories (repository does not delete it).
        // Returns Success if the category was deleted.
        public async Task<OperationResult> DeleteCategoryAsync(Guid id)
        {
            var existingCategory = await categoryRepository.DeleteAsync(id);

            if (existingCategory is null)
            {
                return OperationResult.NotFound;
            }

            if (existingCategory.SubCategories != null && existingCategory.SubCategories.Any())
            {
                return OperationResult.BadRequest;
            }


            return OperationResult.Success;
        }
    }
}