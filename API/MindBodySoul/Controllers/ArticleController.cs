using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.DTO.Article;
using MindBodySoul.Models.Enum;
using MindBodySoul.Repositories.Interface;
using MindBodySoul.Services.Interface;
using System.Security.Claims;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService articleService;

        public ArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetArticleById([FromRoute] Guid id)
        {
            var response = await articleService.GetArticleByIdAsync(id);
            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllArticles([FromQuery] string? search = null)
        {
           var response = await articleService.GetArticlesAsync(search);
            return Ok(response);
        }

      
        [HttpGet("byTag/{tagId:Guid}")]
        public async Task<IActionResult> GetArticlesByTag([FromRoute] Guid tagId)
        {
            var response = await articleService.GetArticlesByTagAsync(tagId);
            return Ok(response);
        }

        [HttpGet("bySubCategory/{subCategoryId:Guid}")]
        public async Task<IActionResult> GetArticlesBySubCategory([FromRoute] Guid subCategoryId)
        {
            var response = await articleService.GetArticlesBySubCategoryAsync(subCategoryId);
            return Ok(response);
        }

        [HttpGet("byCategory/{categoryId:Guid}")]

        public async Task<IActionResult> GetArticlesByCategory([FromRoute] Guid categoryId)
        {
            var response = await articleService.GetArticlesByCategoryAsync(categoryId);
            return Ok(response);
        }

        
        [HttpGet("getLatestArticles")]
        public async Task<IActionResult> GetLatestArticlesAsync([FromQuery] int limit = 8)
        {
            var response = await articleService.GetLatestArticlesByLimit(limit);

            return Ok(response);
        }

       
    }

}

