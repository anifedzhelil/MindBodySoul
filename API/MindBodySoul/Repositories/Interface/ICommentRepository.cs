using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment> UpdateAsync(Comment comment);
        Task<Comment?> DeleteAsync(Guid id);       
        Task<IEnumerable<Comment>> GetAllAsync(Guid articleId);
        Task<Comment?> GetById(Guid id);
        Task<List<Comment>> DeleteRangeAsync(Guid articleId);

    }
}
