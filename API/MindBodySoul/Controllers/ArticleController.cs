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
    public class ArticlesController : Controller
    {

        private readonly IArticleRepository articleRepository;
        private readonly IArticleTagsRepository articleTagsRepository;

        public ArticlesController(IArticleRepository articleRepository, IArticleTagsRepository articleTagsRepository)
        {
            this.articleRepository = articleRepository;
            this.articleTagsRepository = articleTagsRepository;
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

            object value = await articleTagsRepository.AddRangeAsync(articleTags);

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
                CategoryId = article.SubCategory?.Category?.Id,
                SubCategoryId = article.SubCategoryId,
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
        //DELETE: https:/localhost:7108/api/categories{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            var article = await articleRepository.DeleteAsync(id);
            if (article is null)
            {
                return NotFound();
            }

            await articleTagsRepository.DeleteRangeAsync(id);
           
            return Ok();
        }

    }
}
