using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO.Post;

namespace MindBodySoul.Repositories.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostDto>> GetAllAsync(string? search = null); 
        Task<Post?> GetByIdAsync(Guid id);
        Task<Post> CreateAsync(Post post);
        Task<Post?> UpdateAsync(Post post);
    }
}
