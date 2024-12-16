using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment> UpdateAsync(Guid id, string content, DateTime updatedDate);
        Task<Comment?> DeleteAsync(Guid id);       
        Task<IEnumerable<Comment>> GetAllAsync(Guid articleId);
        Task<Comment?> GetById(Guid id);
        Task<List<Comment>> DeleteRangeAsync(Guid articleId);

    }
}
