using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.DTO.Article;
using MindBodySoul.Repositories.Interface;
using MindBodySoul.Services.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;
        private readonly IArticleTagsRepository articleTagsRepository;
        private readonly IArticleService articleService;

        public ArticlesController(IArticleRepository articleRepository,
            IArticleTagsRepository articleTagsRepository,
            IArticleVisitsRepository articleVisitsRepository,
            IArticleService articleService)
        {
            this.articleRepository = articleRepository;
            this.articleTagsRepository = articleTagsRepository;
            this.articleService = articleService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleRequestDto request)
        {
            await articleService.CreateArticleAsync(request);           
            return Ok();
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

        //DELETE: https:/localhost:7108/api/article{id}
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            var responce = await articleService.DeleteArticleAsync(id);

            if (!responce)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditArticle([FromRoute] Guid id, UpdateArticleRequestDto request)
        {
            var response  = await articleService.UpdateArticleAsync(id, request);
            if(!response)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("getLatestArticles")]
        public async Task<IActionResult> GetLatestArticlesAsync([FromQuery] int limit = 8)
        {
            var response = await articleService.GetLatestArticlesByLimit(limit);

            return Ok(response);
        }

    }

}

