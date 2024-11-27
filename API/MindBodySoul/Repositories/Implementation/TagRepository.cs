using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext dbContext;
        public TagRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<(bool isCreated, Tag Tag)> CreateAsync(Tag tag)
        {
            var exestingTag = await dbContext.Tags
                .FirstOrDefaultAsync(t => t.Name.Equals(tag.Name, StringComparison.OrdinalIgnoreCase));

            if (exestingTag != null)
            {
                return (false, exestingTag);
            }

            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();

            return (true, tag);
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var exestingTag = await dbContext.Tags
               .Include(t => t.ArticleTags)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (exestingTag is null)
            {
                return null;
            }

            if (exestingTag.ArticleTags != null && exestingTag.ArticleTags.Any())
            {
                return exestingTag;
            }

            dbContext.Tags.Remove(exestingTag);
            await dbContext.SaveChangesAsync();

            return exestingTag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetById(Guid id)
        {
            return await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dbContext.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);
            
            if (existingTag != null)
            {
                dbContext.Entry(tag).CurrentValues.SetValues(tag);
                await dbContext.SaveChangesAsync();
                return tag;
            }

            return null;
        }
    }
}
