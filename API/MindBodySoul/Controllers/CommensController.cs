using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<IdentityUser> userManager;

        public CommentsController(ICommentRepository commentRepository, UserManager<IdentityUser> userManager)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
        }


        [HttpPost]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDto request)
        {

            var comment = new Comment
            {
                Content = request.Content,
                ArticleId = request.ArticleId,
                CreatedDate = request.CreatedDate,
                UserId = request.UserId,
            };

            await commentRepository.CreateAsync(comment);

            var response = new CommentDto()
            {
                Id = comment.Id,
                Content = comment.Content,
                ArticleId = comment.ArticleId,
                CreatedDate = comment.CreatedDate,
                UserId = comment.UserId,
            };


            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid id)
        {
            var comment = await commentRepository.GetById(id);

            //map domain method to Dto
            if (comment == null)
            {
                return NotFound();
            }

            var response = new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                ArticleId = comment.ArticleId,
                CreatedDate = comment.CreatedDate,
                UserId = comment.UserId,
                UpdatedDate = comment.UpdatedDate,
            };

            return Ok(response);
        }

        [HttpGet("allComments/{articleId:Guid}")]
        public async Task<IActionResult> GetAllComments([FromRoute] Guid articleId)
        {
            var comments = await commentRepository.GetAllAsync(articleId);

            //map domain method to Dto

            var userIds = comments.Select(c => c.UserId).Distinct();

            var response = new List<CommentDto>();
            foreach (var comment in comments)
            {
            var user = await userManager.FindByIdAsync(comment.UserId.ToString());


                response.Add(new CommentDto
                {
                    Id = comment.Id,
                    ArticleId= comment.ArticleId,
                    Content= comment.Content,
                    CreatedDate= comment.CreatedDate, 
                    UserId= comment.UserId,
                    UpdatedDate = comment.UpdatedDate,
                    UserName = user?.UserName,
                });
            }

            return Ok(response);
        }

        //PUT https:/localhost:7108/api/comment{id}
        [HttpPut]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> UpdateComment(UpdateCommentRequestDto request)
        {

            var comment = await commentRepository.UpdateAsync(request.Id, request.Content, request.UpdatedDate);

            if (comment == null)
            {
                return NotFound();
            }
            var response = new CommentDto()
            {
                Id  = comment.Id,
                Content = request.Content,
                ArticleId = comment.ArticleId,
                CreatedDate = comment.CreatedDate,
                UserId = comment.UserId,
                UpdatedDate = comment.UpdatedDate
            };

            return Ok(response);
        }


        //DELETE: https:/localhost:7108/api/categories{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
         public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var comment = await commentRepository.DeleteAsync(id);
            if (comment is null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
