using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO;
using MindBodySoul.Models.DTO.Article;
using MindBodySoul.Models.Enum;
using MindBodySoul.Repositories.Interface;
using MindBodySoul.Services.Interface;

namespace MindBodySoul.Services.Implementation
{
    public class ArticleService : IArticleService
    {
        private const int DefaultLimit = 4;
        private const int MaxLimit = 20;
        private const int ExcerptLength = 30;

        private readonly IArticleRepository articleRepository;
        private readonly IArticleTagsRepository articleTagsRepository;
        public ArticleService(IArticleRepository articleRepository, IArticleTagsRepository articleTagsRepository)
        {
            this.articleRepository = articleRepository;
            this.articleTagsRepository = articleTagsRepository;
        }

        private ArticleDto GetArticle(Article article)
        {
            var articleDto = new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                CreatedDate = article.CreatedDate,
                UpdatedDate = article.UpdatedDate,
            };

            return articleDto;
        }

        private ArticleDetailsDto GetArticleDetails(Article article)
        {
            var articleDetailsDto = new ArticleDetailsDto
            {
                Id = article.Id,
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

            return articleDetailsDto;
        }

        private LatestArticleDto GetLatestArticle(Article article)
        {

                var response = new LatestArticleDto
                {
                    Id = article.Id,
                    Title = article.Title,
                    Excerpt = article.Content.Length > ExcerptLength
                                ? article.Content.Substring(0, ExcerptLength) + "..."
                                : article.Content,
                    ImageUrl = article.ImageUrl
                };
            

            return response;
        }

        public async Task CreateArticleAsync(CreateArticleRequestDto articleRequest, Guid userId)
        {
            var article = new Article
            {
                Title = articleRequest.Title,
                Content = articleRequest.Content,
                SubCategoryId = articleRequest.SubCategoryId,
                ImageUrl = articleRequest.ImageUrl,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            var response = await articleRepository.CreateAsync(article);

            var articleTags = articleRequest.TagsIDs.Select(tagId => new ArticleTags
            {
                ArticleId = response.Id,
                TagId = tagId
            }).ToList();

            await articleTagsRepository.AddRangeAsync(articleTags);
        }

        public async Task<OperationResult> UpdateArticleAsync(Guid articleId, UpdateArticleRequestDto articleRequest, Guid userId)
        {
            var existingArticle = await articleRepository.GetByIdAsync(articleId);

            if (existingArticle == null)
            {
                return OperationResult.NotFound;
            }

            if(existingArticle.UserId != userId)
            {
                return OperationResult.Forbidden;
            }

            var article = new Article
            {
                Id = articleId,
                Title = articleRequest.Title,
                Content = articleRequest.Content,
                ImageUrl = articleRequest.ImageUrl,
                SubCategoryId = articleRequest.SubCategoryId,
                // Required by the model, not copied by UpdateAsync.
                UserId = userId,
                UpdatedDate = DateTime.UtcNow,
            };

            var response = await articleRepository.UpdateAsync(article);

            if (articleRequest.DeletedTags != null)
            {
                foreach (Guid tagId in articleRequest.DeletedTags)
                {
                    await articleTagsRepository.DeleteAsync(articleId, tagId);
                }
            }
            
            var existingTagIds = await articleTagsRepository.GetTagIdsAsync(articleId);
            var uniqueTagIds = articleRequest.TagsIDs?.Except(existingTagIds);

            if (uniqueTagIds != null)
            {
                var articleTags = uniqueTagIds.Select(tagId => new ArticleTags
                {
                    ArticleId = articleId,
                    TagId = tagId
                }).ToList();

                await articleTagsRepository.AddRangeAsync(articleTags);
            }

            return OperationResult.Success;

        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync(string? search = null)
        {
            var articles = await articleRepository.GetAllAsync(search);
            var response = articles.Select(article => GetArticle(article)).ToList();

            return response;
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesByTagAsync(Guid tagId)
        {
            var articles = await articleRepository.GetAllByTagAsync(tagId);
            var response = articles.Select(article => GetArticle(article)).ToList();

            return response;
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesBySubCategoryAsync(Guid subCategoryId)
        {
            var articles = await articleRepository.GetAllBySubCategoryAsync(subCategoryId);
            var response = articles.Select(article => GetArticle(article)).ToList();

            return response;
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesByCategoryAsync(Guid categoryId)
        {
            var articles = await articleRepository.GetAllByCategoryAsync(categoryId);
            var response = articles.Select(article => GetArticle(article)).ToList();

            return response;
        }

        public async Task<ArticleDetailsDto?> GetArticleByIdAsync(Guid articleId)
        {
            var article = await articleRepository.GetByIdAsync(articleId);

            if (article == null)
            {
                return null;
            }
            
            var response = GetArticleDetails(article);
            return response;
        }

        public async Task<IEnumerable<LatestArticleDto>> GetLatestArticlesByLimit(int limit)
        {
            limit = Math.Clamp(limit, DefaultLimit, MaxLimit);

            var articles = await articleRepository.GetLatestArticlesAsync(limit);
            var response = articles.Select(article => GetLatestArticle(article)).ToList();

            return response;
        }

        public async Task<OperationResult> DeleteArticleAsync(Guid articleId, Guid userId)
        {
            var existingArticle = await articleRepository.GetByIdAsync(articleId);

            if (existingArticle == null)
            {
                return OperationResult.NotFound;
            }
            if (existingArticle.UserId != userId)
            {
                return OperationResult.Forbidden;
            }

            var response = await articleRepository.DeleteAsync(articleId);

            await articleTagsRepository.DeleteRangeAsync(articleId);

            return OperationResult.Success;
        }
    }
}
