using MindBodySoul.Helpers;
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
        public ArticleService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
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
                Tags = article.ArticleTags!
                        .Select(at => new TagDto
                        {
                            Id = at.Tag!.Id,
                            Name = at.Tag!.Name
                        })
                    .ToList()
            };

            return articleDetailsDto;
        }

        private LatestArticleDto GetLatestArticle(Article article)
        {
            var content = HtmlTextHelper.ToPlainText(article.Content);


            var response = new LatestArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Excerpt = content.Length > ExcerptLength
                            ? content.Substring(0, ExcerptLength) + "..."
                            : content,
                ImageUrl = article.ImageUrl
            };


            return response;
        }

        private ArticleEditDto GetArticleEdit(Article article)
        {
            var articleEditDto = new ArticleEditDto
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug,
                Content = article.Content,
                ImageUrl = article.ImageUrl,
                IsPublished = article.IsPublished,
                CategoryId = article.SubCategory?.Category?.Id,
                SubCategoryId = article.SubCategoryId,
                Tags = article.ArticleTags!
                    .Select(at => new TagDto
                    {
                        Id = at.Tag!.Id,
                        Name = at.Tag!.Name
                    })
                    .ToList()
            };
            return articleEditDto;
        }

        private AdminArticleListItemDto GetAdminListItem(Article article)
        {
            var articleListItem = new AdminArticleListItemDto
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug,
                ImageUrl = article.ImageUrl,
                IsPublished = article.IsPublished,
                CreatedDate = article.CreatedDate,
                UpdatedDate = article.UpdatedDate,
                PublishedDate = article.PublishedDate,
                IsArchived = article.IsArchived,
                TotalVisitCount = article.TotalVisitCount,
                UniqueVisitCount = article.UniqueVisitCount,
            };
            return articleListItem;
        }


        public async Task<OperationResult> CreateArticleAsync(CreateArticleRequestDto articleRequest, Guid userId)
        {
            var articleExists = await articleRepository.SlugExistsAsync(null, articleRequest.Slug);
            if (articleExists)
            {
                return OperationResult.Conflict;
            }

            var now = DateTime.UtcNow;
            var article = new Article
            {
                Title = articleRequest.Title,
                Content = articleRequest.Content,
                Slug = articleRequest.Slug,
                ContentText = HtmlTextHelper.ToPlainText(articleRequest.Content),
                SubCategoryId = articleRequest.SubCategoryId,
                ImageUrl = articleRequest.ImageUrl,
                IsPublished = articleRequest.IsPublished,
                PublishedDate = articleRequest.IsPublished ? now : null,
                UserId = userId,
                CreatedDate = now,
                ArticleTags = articleRequest.TagsIDs.Select(tagId => new ArticleTags
                {
                    TagId = tagId
                }).ToList()
            };

            await articleRepository.CreateAsync(article);

            return OperationResult.Success;
        }

        public async Task<OperationResult> UpdateArticleAsync(Guid articleId, UpdateArticleRequestDto articleRequest)
        {
            var existingArticle = await articleRepository.GetByIdForUpdateAsync(articleId);
            var now = DateTime.UtcNow;

            if (existingArticle == null)
            {
                return OperationResult.NotFound;
            }

            var slugExists = await articleRepository.SlugExistsAsync(articleId, articleRequest.Slug);

            if (slugExists)
            {
                return OperationResult.Conflict;
            }

            existingArticle.Title = articleRequest.Title;
            existingArticle.Content = articleRequest.Content;
            existingArticle.Slug = articleRequest.Slug;
            existingArticle.ContentText = HtmlTextHelper.ToPlainText(articleRequest.Content);
            existingArticle.ImageUrl = articleRequest.ImageUrl;
            existingArticle.SubCategoryId = articleRequest.SubCategoryId;
            existingArticle.UpdatedDate = now;
            if (existingArticle.PublishedDate == null && articleRequest.IsPublished)
            {
                existingArticle.PublishedDate = now;
            }
            existingArticle.IsPublished = articleRequest.IsPublished;

            if (articleRequest.DeletedTags != null)
            {

                var tagsToRemove = existingArticle.ArticleTags!
                    .Where(at => articleRequest.DeletedTags.Contains(at.TagId))
                    .ToList();

                foreach (var articleTag in tagsToRemove)
                {
                    existingArticle.ArticleTags!.Remove(articleTag);
                }
            }

            var existingTagIds = existingArticle.ArticleTags!.Select(at => at.TagId).ToList();


            var uniqueTagIds = articleRequest.TagsIDs?.Except(existingTagIds);

            if (uniqueTagIds != null)
            {
                foreach (var tagId in uniqueTagIds)
                {
                    existingArticle.ArticleTags!.Add(new ArticleTags { TagId = tagId });
                }
            }

            await articleRepository.SaveChangesAsync();

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
            var article = await articleRepository.GetPublishedByIdAsync(articleId);

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

        public async Task<IEnumerable<AdminArticleListItemDto>> GetFilteredForAdmin(ArticleStatusFilter statusFilter)
        {
            var articles = await articleRepository.GetFilteredForAdminAsync(statusFilter);
            var response = articles.Select(article => GetAdminListItem(article)).ToList();

            return response;
        }

        public async Task<OperationResult> DeleteArticleAsync(Guid articleId)
        {
            var existingArticle = await articleRepository.GetByIdForUpdateAsync(articleId);

            if (existingArticle == null)
            {
                return OperationResult.NotFound;
            }
        
            await articleRepository.DeleteAsync(existingArticle);

            return OperationResult.Success;
        }

        public async Task<OperationResult> UpdateArchiveStatusAsync(Guid articleId, bool isArchived)
        {
            var existingArticle = await articleRepository.GetByIdForUpdateAsync(articleId);

            if (existingArticle == null)
            {
                return OperationResult.NotFound;
            }
          
            existingArticle.IsArchived = isArchived;
            await articleRepository.SaveChangesAsync();
            return OperationResult.Success;
        }

        public async Task<ArticleEditDto?> GetArticleForEditAsync(Guid articleId)
        {
            var article = await articleRepository.GetByIdForEditAsync(articleId);

            if (article == null)
            {
                return null; 
            }

            return GetArticleEdit(article);
        }
    }
}
