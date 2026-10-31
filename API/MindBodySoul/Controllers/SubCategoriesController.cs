using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.DTO.SubCategory;
using MindBodySoul.Models.Enum;
using MindBodySoul.Services.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : Controller
    {
        private readonly ISubCategoryService subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            this.subCategoryService = subCategoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryRequestDto request)
        {
            var response = await subCategoryService.CreateSubCategoryAsync(request);

            if (response == OperationResult.UrlHandleConflict)
            {
                return Conflict("Този URL адрес вече е зает. Променете го.");
            }
            else if (response == OperationResult.NameConflict)
            {
                return Conflict("Името вече е зает. Променете го.");
            }

            return Ok();
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetSubCategoryById([FromRoute] Guid id)
        {
            var subCategory = await subCategoryService.GetSubCategoryAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return Ok(subCategory);
        }

        // Admin endpoint: returns all subcategories, including those without articles.
        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var response = await subCategoryService.GetSubCategoriesAsync();

            return Ok(response);
        }

        // Public endpoint for readers: returns only subcategories
        // that have at least one published, non-archived article.
        [HttpGet("Category/{categoryId:Guid}")]
        public async Task<IActionResult> GetSubCategoriesByCategoryId([FromRoute] Guid categoryId)
        {
            var response = await subCategoryService.GetSubCategoriesByCategoryIdAsync(categoryId);

            return Ok(response);
        }


        // Admin endpoint: updates an existing subcategory.
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditSubCategory([FromRoute] Guid id, UpdateSubCategoryRequestDto request)
        {
            var response = await subCategoryService.UpdateSubCategoryAsync(id, request);

            if (response == OperationResult.NotFound)
            {
                return NotFound();
            }

            if (response == OperationResult.UrlHandleConflict)
            {
                return Conflict("Този URL адрес вече е зает. Променете го.");
            }
            else if (response == OperationResult.NameConflict)
            {
                return Conflict("Името вече е зает. Променете го.");
            }

            return Ok();
        }

        // Admin endpoint: deletes a subcategory only if it has no articles.
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] Guid id)
        {
            var response = await subCategoryService.DeleteSubCategoryAsync(id);

            if (response == OperationResult.NotFound)
            {
                return NotFound();
            }
            else if (response == OperationResult.BadRequest)
            {
                return BadRequest("Не може да се изтрие подкатегории със съществуващи статии.");
            }

            return Ok();
        }

        // Public endpoint for readers: returns all subcategories
        // that have at least one published, non-archived article.
        [HttpGet("published")]
        public async Task<IActionResult> GetPublishedSubCategories()
        {
            var response = await subCategoryService.GetPublishedSubCategoriesAsync();

            return Ok(response);
        }
    }
}
