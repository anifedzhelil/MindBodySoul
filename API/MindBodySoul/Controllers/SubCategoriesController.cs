using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO;
using MindBodySoul.Repositories.Implementation;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoryRepository subCategoryRepository;

        public SubCategoriesController(ISubCategoryRepository subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryRequestDto reqest)
        {

            var subCategory = new SubCategory
            {
                Name = reqest.Name,
                UrlHandle = reqest.UrlHandle,
                Icon = reqest.Icon,
                CategoryId = reqest.CategoryId,
            };

            await subCategoryRepository.CreateAsync(subCategory);

            var response = new SubCategoryDto()
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                CategoryId = subCategory.CategoryId,
                UrlHandle = subCategory.UrlHandle,
                Icon = subCategory.Icon
            };

            return Ok(response);
        }


        [HttpGet("{id:Guid}")]

        public async Task<IActionResult> GetSubCategoryById([FromRoute] Guid id)
        {
            var subCategory = await subCategoryRepository.GetById(id);

            //map domain method to Dto
            if (subCategory == null)
            {
                return NotFound();
            }

            var response = new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                UrlHandle = subCategory.UrlHandle,
                CategoryId = subCategory.CategoryId,
                Icon = subCategory.Icon
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var subCategories = await subCategoryRepository.GetAllAsync();

            //map domain method to Dto

            var response = new List<SubCategoryDto>();
            foreach (var subCategory in subCategories)
            {
                response.Add(new SubCategoryDto
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    Category = subCategory.Category,
                    UrlHandle = subCategory.UrlHandle,
                    Icon =  subCategory.Icon,
                    CategoryId = subCategory.CategoryId
                    
                });
            }

            return Ok(response);
        }

      
        [HttpGet("Category/{categoryId:Guid}")]

        public async Task<IActionResult> GetSubCategoriesByCategoryId([FromRoute] Guid categoryId)
        {
            var subCategories = await subCategoryRepository.GetByCategoryId(categoryId);

            //map domain method to Dto

            var response = new List<SubCategoryDto>();
            foreach (var subCategory in subCategories)
            {
                response.Add(new SubCategoryDto
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                });
            }

            return Ok(response);
        }

        //PUT https:/localhost:7108/api/subCategories{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditSubCategory([FromRoute] Guid id, UpdateSubCategoryRequestDto request)
        {
            var subCategory = new SubCategory
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
                Icon = request.Icon,
                CategoryId = request.CategoryId,
            };

            subCategory = await subCategoryRepository.UpdateAsync(subCategory);

            if (subCategory == null)
            {
                return NotFound();
            }
            var response = new SubCategoryDto()
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                UrlHandle = subCategory.UrlHandle,
                CategoryId = subCategory.CategoryId,
                Icon = subCategory.Icon,
               // Category = subCategory.Category,
            };

            return Ok(response);

        }

        //DELETE: https:/localhost:7108/api/subCategories{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] Guid id)
        {
            var subCategory = await subCategoryRepository.DeleteAsync(id);
            if (subCategory is null)
            {
                return NotFound();
            }

            if (subCategory.Articles != null && subCategory.Articles.Any())
            {
                //Cannot delete category with existing Articles.
                return BadRequest("Не може да се изтрие подкатегории със съществуващи статии.");
            }

            var response = new SubCategoryDto()
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                UrlHandle = subCategory.UrlHandle,
                CategoryId = subCategory.CategoryId,
                Icon = subCategory.Icon,
               // Category = subCategory.Category,
            };

            return Ok(response);
        }

    }
}
