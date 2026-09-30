using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO;
using MindBodySoul.Models.DTO.Article;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private const int DefaultLimit = 4;
        private const int MaxLimit = 20;
        private const int ExcerptLength = 30;

        private readonly IArticleRepository articleRepository;
        private readonly IArticleTagsRepository articleTagsRepository;

        public ArticlesController(IArticleRepository articleRepository,
            IArticleTagsRepository articleTagsRepository,
            IArticleVisitsRepository articleVisitsRepository)
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
       
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetArticleById([FromRoute] Guid id)
        {
            var article = await articleRepository.GetByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            var response = new ArticleDetailsDto
            {
                Id = id,
                UserId = article.UserId,
                Title = article.Title,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                CreatedDate = article.CreatedDate,
                UpdatedDate = article.UpdatedDate,
                CategoryName = article.SubCategory?.Category?.Name,
                CategoryId = article.SubCategory?.Category?.Id,
                SubCategoryId = article.SubCategoryId,
                SubCategoryName = article.SubCategory?.Name,
                TotalVisitCount = article.TotalVisitCount,
                UniqueVisitCount = article.UniqueVisitCount,
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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllArticles([FromQuery] string? search = null)
        {

            var articles = await articleRepository.GetAllAsync(search);

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
                });
            }
            return Ok(response);
        }

      
        [HttpGet("byTag/{tagId:Guid}")]
        public async Task<IActionResult> GetArticlesByTag([FromRoute] Guid tagId)
        {
            var articles = await articleRepository.GetAllByTagAsync(tagId);

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
                });
            }
            return Ok(response);
        }

        [HttpGet("bySubCategory/{subCategoryId:Guid}")]
        public async Task<IActionResult> GetArticlesBySubCategory([FromRoute] Guid subCategoryId)
        {
            var articles = await articleRepository.GetAllBySubCategoryAsync(subCategoryId);
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
                });
            }
            return Ok(response);
        }

        [HttpGet("byCategory/{categoryId:Guid}")]

        public async Task<IActionResult> GetArticlesByCategory([FromRoute] Guid categoryId)
        {
            var articles = await articleRepository.GetAllByCategoryAsync(categoryId);

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
                });
            }
            return Ok(response);
        }

        //DELETE: https:/localhost:7108/api/article{id}
        [HttpDelete("{id:Guid}")]
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

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditArticle([FromRoute] Guid id, UpdateArticleRequestDto request)
        {
            var article = new Article
            {
                Id = id,
                Title = request.Title,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                SubCategoryId = request.SubCategoryId,
                UserId = request.UserId,
                UpdatedDate = request.UpdatedDate
            };

            article = await articleRepository.UpdateAsync(article);


            if (article == null)
            {
                return NotFound();
            }
            if (request.DeletedTags != null)
            {
                foreach (Guid tagId in request.DeletedTags)
                {
                    await articleTagsRepository.DeleteAsync(article.Id, tagId);
                }
            }

            if (request.TagsIDs != null)
            {
                var articleTags = request.TagsIDs.Select(tagId => new ArticleTags
                {
                    ArticleId = article.Id,
                    TagId = tagId
                }).ToList();

                object value = await articleTagsRepository.AddRangeAsync(articleTags);
            }

            return Ok();
        }

        [HttpGet("getLatestArticles")]
        public async Task<IActionResult> GetLatestArticlesAsync([FromQuery] int limit = 8)
        {

            limit = Math.Clamp(limit, DefaultLimit, MaxLimit);

            var articles = await articleRepository.GetLatestArticlesAsync(limit);

            var response = new List<LatestArticleDto>();

            foreach (var article in articles)
            {
                response.Add(new LatestArticleDto
                {
                    Id = article.Id,
                    Title = article.Title,
                    Excerpt = article.Content.Length > ExcerptLength
                                ? article.Content.Substring(0, ExcerptLength) + "..."
                                : article.Content,
                    ImageUrl = article.ImageUrl
                });
            }
            return Ok(response);
        }

    }

}

