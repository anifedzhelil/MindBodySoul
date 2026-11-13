using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO.Comment;
using MindBodySoul.Models.Enum;
using MindBodySoul.Repositories.Implementation;
using MindBodySoul.Repositories.Interface;
using MindBodySoul.Services.Interface;

namespace MindBodySoul.Services.Implementation
{
    public class CommentService: ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IArticleRepository articleRepository;


        public CommentService(ICommentRepository commentRepository, UserManager<IdentityUser> userManager, IArticleRepository articleRepository)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
            this.articleRepository = articleRepository;
        }

        private CommentDto GetComment(Comment comment)
        {
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                ArticleId = comment.ArticleId,
                CreatedDate = comment.CreatedDate,
                UpdatedDate = comment.UpdatedDate,
                UserId = comment.UserId,
            };

            return commentDto;
        }

     
        public async Task<OperationResult> CreateCommentAsync(CreateCommentRequestDto request, Guid userId)
        {
            var isPublished = await articleRepository.IsPublishedAsync(request.ArticleId);
            if (!isPublished)
            {
                // Comments are allowed only on published, non-archived articles
                return OperationResult.NotFound;
            }

            var comment = new Comment
            {
                Content = request.Content,
                ArticleId = request.ArticleId,
                CreatedDate = DateTime.UtcNow,
                UserId = userId,
            };

            await commentRepository.CreateAsync(comment);

            return OperationResult.Success;
        }

         public async Task<IEnumerable<CommentDto>> GetCommentsByArticleAsync(Guid articleId)
        {
            var comments = await commentRepository.GetAllAsync(articleId);

            var userIds = comments.Select(c => c.UserId.ToString()).Distinct().ToList();

            // Load all comment authors in one query instead of one query per comment
            var userNames = await userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.UserName);

            var response = comments.Select(comment =>
            {
                var commentDto = GetComment(comment);
                commentDto.UserName = userNames.GetValueOrDefault(comment.UserId.ToString());
                return commentDto;
            }).ToList();

            return response;
        }

        public async Task<OperationResult> UpdateCommentAsync(UpdateCommentRequestDto request, Guid userId)
        {
            var existingComment = await commentRepository.GetById(request.Id);

            if (existingComment == null)
            {
                return OperationResult.NotFound;
            }

            if (existingComment.UserId != userId)
            {
                // Only the author can edit the comment
                return OperationResult.Forbidden;
            }

            existingComment.Content = request.Content;
            existingComment.UpdatedDate = DateTime.UtcNow;

            await commentRepository.SaveChangesAsync();

            return OperationResult.Success;
        }

        public async Task<OperationResult> DeleteCommentAsync(Guid commentId, Guid userId)
        {
            var existingComment = await commentRepository.GetById(commentId);

            if (existingComment == null)
            {
                return OperationResult.NotFound;
            }

            if (existingComment.UserId != userId)
            {
                // Only the author can delete the comment
                return OperationResult.Forbidden;
            }

            await commentRepository.DeleteAsync(existingComment);

            return OperationResult.Success;
        }
    }
}
