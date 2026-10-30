using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.DTO.Category;
using MindBodySoul.Models.Enum;
using MindBodySoul.Services.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService= categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto reqest)
        {
          
            var response = await categoryService.CreateCategoryAsync(reqest);
            if (response == OperationResult.UrlHandleConflict)
            {
                return Conflict("Този URL адрес вече е зает. Променете го.");

            }
            else if (response == OperationResult.NameConflict) {
                return Conflict("Името вече е зает. Променете го.");
            }

            return Ok();           
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await categoryService.GetCategorisAsync();
            return Ok(response);
        }

        //PUT https:/localhost:7108/api/categories{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            var response  = await categoryService.UpdateCategoryAsync(id, request);

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


        //DELETE: https:/localhost:7108/api/categories{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
         public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var response = await categoryService.DeleteCategoryAsync(id);
            if (response == OperationResult.NotFound)
            {
                return NotFound();
            }
            else if(response == OperationResult.BadRequest)

            {
                return BadRequest("Не може да се изтрие категория със съществуващи подкатегории.");
            }

            return Ok();
        }

    }
}
