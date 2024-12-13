using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : Controller
    {

        private readonly IArticleRepository articleRepository;
        private readonly IArticleTagRepository articleTagRepository;

        public ArticlesController(IArticleRepository articleRepository, IArticleTagRepository articleTagRepository)
        {
            this.articleRepository = articleRepository;
            this.articleTagRepository = articleTagRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleRequestDto request)
        {

                var article = new Article
            {
                Title = request.Title,
                Content = request.Content,
                SubCategoryId = request.SubCategoryId,
                ImageUrl = request.ImageUrl,
                UserId = request.UserId,
                CreatedDate = request.CreatedDate
            };

            var response = await articleRepository.CreateAsync(article);

            var articleTags = request.TagsIDs.Select(tagId => new ArticleTags
            {
                ArticleId = response.Id,
                TagId = tagId
            }).ToList();

            object value = await articleTagRepository.AddRangeAsync(articleTags);

            return Ok();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetArticleById([FromRoute] Guid id)
        {
            var article = await articleRepository.GetById(id);

            if (article == null)
            {
                return NotFound();
            }

            var response = new ArticleDetailsDto
            {
                Id = id,
                Title = article.Title,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                CreatedDate = article.CreatedDate,
                UpdatedDate = article.UpdatedDate,
                CategoryName = article.SubCategory?.Category?.Name,
                SubCategoryName = article.SubCategory?.Name,
                Tags = article.ArticleTags
                       .Select(at => new TagDto
                       {
                           Id = at.Tag.Id,
                           Name = at.Tag.Name
                       })
        .ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles() 
        {
            var articles = await articleRepository.GetAllAsync();

            var response = new List<ArticleDto>();

            foreach (var article in articles)
            {
                response.Add(new ArticleDto
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    ImageUrl = article.ImageUrl,
                    CreatedDate = article.CreatedDate,
                    UpdatedDate = article.UpdatedDate,
                    ArticleTags = article.ArticleTags
                });
            }
            return Ok(response);
        }

    }
}