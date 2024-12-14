using Azure.Core;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
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

        public CommentsController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }


        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDto reqest)
        {

            var comment = new Comment
            {
                Content = reqest.Content,
                ArticleId = reqest.ArticleId,
                CreatedDate = reqest.CreatedDate,
                UserId = reqest.UserId,
            };

            await commentRepository.CreateAsync(comment);

            var response = new CommentDto()
            {
                Id = comment.Id,
                Content = reqest.Content,
                ArticleId = reqest.ArticleId,
                CreatedDate = reqest.CreatedDate,
                UserId = reqest.UserId,
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

        [HttpGet]
        [Route("{articleId:Guid}")]
        
        public async Task<IActionResult> GetAllComments([FromRoute] Guid articleId)
        {
            var comments = await commentRepository.GetAllAsync(articleId);

            //map domain method to Dto

            var response = new List<CommentDto>();
            foreach (var comment in comments)
            {
                response.Add(new CommentDto
                {
                    Id = comment.Id,
                    ArticleId= comment.ArticleId,
                    Content= comment.Content,
                    CreatedDate= comment.CreatedDate, UserId= comment.UserId,
                    UpdatedDate = comment.UpdatedDate   

                });
            }

            return Ok(response);
        }

        //PUT https:/localhost:7108/api/comment{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, UpdateCommentRequestDto request)
        {
            var comment = new Comment
            {
                Id = id,
                Content = request.Content,
                UpdatedDate = request.UpdatedDate,
            };

            comment = await commentRepository.UpdateAsync(comment);

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
        [Authorize(Roles = "Writer")]
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
