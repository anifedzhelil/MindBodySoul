using MindBodySoul.Models.Domain;

namespace MindBodySoul.Repositories.Interface
{
    public interface ITagRepository
    {
        Task<(bool isCreated, Tag Tag)> CreateAsync(Tag tag);
        Task<(bool isUpdated, Tag? Tag)> UpdateAsync(Tag tag);
        Task<Tag> DeleteAsync(Guid id);

        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag?> GetById(Guid id);

    }
}
