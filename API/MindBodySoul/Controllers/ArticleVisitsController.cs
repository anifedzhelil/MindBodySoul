using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Implementation;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/articlevisits")]
    [ApiController]
    public class ArticleVisitsController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly IArticleVisitsRepository articleVisitsRepository;
        public ArticleVisitsController(IArticleRepository articleRepository,
            IArticleTagsRepository articleTagsRepository,
            IArticleVisitsRepository articleVisitsRepository)
        {
            this.articleRepository = articleRepository;
            this.articleVisitsRepository = articleVisitsRepository;
        }

       

        [HttpGet]
        [Route("test")]

        public async Task<IActionResult> Test()
        {
            

            return Ok();
        }

        [HttpPut]
        [Route("register/{articleId:Guid}")]

        public async Task<IActionResult> RegisterVisit([FromRoute] Guid articleId)
        {
            var article = await articleRepository.GetById(articleId);
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            
                if (article == null)
            {
                return NotFound();
            }

            if (Guid.TryParse(userIdString, out var userId))
            {
                var articleVisit = await articleVisitsRepository.GetAsync(articleId, userId);

                if (articleVisit == null)
                {
                    articleVisit = new ArticleVisit
                    {
                        ArticleId = articleId,
                        UserId = userId,
                        VisitDate = DateTime.Now,
                    };

                    await articleVisitsRepository.AddAsync(articleVisit);

                    article.UniqueVisitCount++;
                }
            }

            article.TotalVisitCount++;

            await articleRepository.UpdateAsync(article);

            return Ok();
        }
        /*
        //DELETE: https:/localhost:7108/api/deleteVisit{articleId}
        [HttpDelete]
        [Route("{articleId:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteArticleVisit([FromRoute] Guid articleId)
        {
            var article = await articleVisitsRepository.DeleteAsync(articleId);
            if (article is null)
            {
                return NotFound();
            }

            await articleVisitsRepository.DeleteAsync(articleId);

            return Ok();
        }*/
    }
}
