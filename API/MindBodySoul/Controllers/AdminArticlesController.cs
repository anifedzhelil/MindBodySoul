using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.DTO.Article;
using MindBodySoul.Models.Enum;
using MindBodySoul.Services.Interface;
using System.Security.Claims;

namespace MindBodySoul.Controllers
{
    [Authorize(Roles = "Writer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminArticlesController : ControllerBase
    {
        private readonly IArticleService articleService;
        public AdminArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        private bool TryGetUserId(out Guid userId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out userId);
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetArticleForEdit([FromRoute] Guid id)
        {
          
            var response = await articleService.GetArticleForEditAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("getArticleForPreview/{id:Guid}")]
        public async Task<IActionResult> GetArticleForPreview([FromRoute] Guid id)
        {

            var response = await articleService.GetArticleForPreviewAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpGet("getFilteredForAdmin/{status}")]
        public async Task<IActionResult> GetFilteredForAdmin([FromRoute] ArticleStatusFilter status)
        {
            var response = await articleService.GetFilteredForAdmin(status);

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleRequestDto request)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            var result = await articleService.CreateArticleAsync(request, userId);
            if (result == OperationResult.Conflict)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
           
            var response = await articleService.DeleteArticleAsync(id);

            if (response == OperationResult.NotFound)
            {
                return NotFound();
            }
          
            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> EditArticle([FromRoute] Guid id, UpdateArticleRequestDto request)
        {
            var response = await articleService.UpdateArticleAsync(id, request);
            if (response == OperationResult.NotFound)
            {
                return NotFound();
            }
            else if (response == OperationResult.Conflict)
            {
                return Conflict("Този URL адрес вече е зает. Променете го.");
            }
            else if (response == OperationResult.ArchivedConflict)
            {
                return Conflict("Архивирана статия не може да стане чернова.");
            }
            return Ok();
        }

        [HttpPut("archive/{id:Guid}")]
        public async Task<IActionResult> EditArchiveStatusAsync([FromRoute] Guid id, [FromQuery] bool isArchived)
        {
           
            var response = await articleService.UpdateArchiveStatusAsync(id, isArchived);

            if (response == OperationResult.Conflict)
            {
                return Conflict("Невалидна операция при архивиране");
            }

            if (response == OperationResult.NotFound)
            {
                return NotFound();
            }
         
            return Ok(isArchived);
        }

    }
}
