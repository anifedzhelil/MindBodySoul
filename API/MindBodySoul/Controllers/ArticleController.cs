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
        private readonly IArticleTagsRepository articleTagsRepository;
        private readonly IArticleVisitsRepository articleVisitsRepository;

        public ArticlesController(IArticleRepository articleRepository,
            IArticleTagsRepository articleTagsRepository,
            IArticleVisitsRepository articleVisitsRepository)
        {
            this.articleRepository = articleRepository;
            this.articleTagsRepository = articleTagsRepository;
            this.articleVisitsRepository = articleVisitsRepository;
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
        /*
                [HttpPut]
                [Route("registerVisit{id:Guid}")]

                public async Task<IActionResult> RegisterVisit([FromRoute] Guid id,[FromBody] Guid userId)
                {
                    var article = await articleRepository.GetById(id);

                    if (article == null)
                    {
                        return NotFound();
                    }

                    var articleVisit = await articleVisitsRepository.GetAsync(id);

                    if (articleVisit == null)
                    {
                        articleVisit = new ArticleVisit
                        {
                            ArticleId = id,
                            UserId = userId,
                            VisitDate = DateTime.Now,
                        };

                        await articleVisitsRepository.AddAsync(articleVisit);

                        article.UniqueVisitCount++;
                    }

                    article.TotalVisitCount++;

                    await articleRepository.UpdateAsync(article);

                    return Ok();
                }*/

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
        [Route("getAll/{search}")]
        public async Task<IActionResult> GetFilteredArticles([FromRoute] string search)
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
                    ArticleTags = article.ArticleTags
                });
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("getAll")]
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
                    // ArticleTags = article.ArticleTags
                });
            }
            return Ok(response);
        }

        [HttpGet("bySubCategory/{subCategoryId:Guid}")]
        public async Task<IActionResult> GetArticlesBySubCategory([FromRoute] Guid subCategoryId)
        {
            var articles = await articleRepository.GetAllBySubategoryAsync(subCategoryId);

            var response = new List<ArticleDto>();

            if (articles != null)
            {
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

            return BadRequest();
        }

        [HttpGet("byCategory/{categoryId:Guid}")]

        public async Task<IActionResult> GetArticlesByCategory([FromRoute] Guid categoryId)
        {
            var articles = await articleRepository.GetAllByCategoryAsync(categoryId);

            var response = new List<ArticleDto>();
            if (articles != null)
            {
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

            return BadRequest();
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

        [HttpPut]
        [Route("{id:Guid}")]
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

    }

}

