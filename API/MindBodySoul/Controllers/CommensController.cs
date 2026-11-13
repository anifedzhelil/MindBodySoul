using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO.Comment;
using MindBodySoul.Models.Enum;
using MindBodySoul.Repositories.Interface;
using MindBodySoul.Services.Interface;
using System.Security.Claims;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;


        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDto request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }
            var result = await commentService.CreateCommentAsync(request, userId);

            if (result == OperationResult.NotFound)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpGet("allComments/{articleId:Guid}")]
        public async Task<IActionResult> GetAllComments([FromRoute] Guid articleId)
        {
            var response = await commentService.GetCommentsByArticleAsync(articleId);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentRequestDto request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await commentService.UpdateCommentAsync(request, userId);

            if (result == OperationResult.NotFound)
            {
                return NotFound();
            }

            if (result == OperationResult.Forbidden)
            {
                return Forbid();
            }

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var result = await commentService.DeleteCommentAsync(id, userId);

            if (result == OperationResult.NotFound)
            {
                return NotFound();
            }

            if (result == OperationResult.Forbidden)
            {
                return Forbid();
            }

            return Ok();
        }

    }
}
