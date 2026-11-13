using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllAsync(Guid articleId);
        Task<Comment?> GetById(Guid id);
        Task SaveChangesAsync();
    }
}
