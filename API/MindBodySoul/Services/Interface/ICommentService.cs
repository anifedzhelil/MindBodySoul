using MindBodySoul.Models.DTO.Comment;
using MindBodySoul.Models.Enum;

namespace MindBodySoul.Services.Interface
{
    public interface ICommentService
    {
        Task<OperationResult> CreateCommentAsync(CreateCommentRequestDto request, Guid userId);
        Task<IEnumerable<CommentDto>> GetCommentsByArticleAsync(Guid articleId);
        Task<OperationResult> UpdateCommentAsync(UpdateCommentRequestDto request, Guid userId);
        Task<OperationResult> DeleteCommentAsync(Guid commentId, Guid userId);
    }
}
