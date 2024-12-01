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
            try
            {
                var existingTag = await dbContext.Tags
                    .FirstOrDefaultAsync(t => t.Name.ToLower() == tag.Name.ToLower());

                if (existingTag != null)
                {
                    return (false, existingTag);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();

            return (true, tag);
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dbContext.Tags
               .Include(t => t.ArticleTags)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (existingTag is null)
            {
                return null;
            }

            if (existingTag.ArticleTags != null && existingTag.ArticleTags.Any())
            {
                return existingTag;
            }

            dbContext.Tags.Remove(existingTag);
            await dbContext.SaveChangesAsync();

            return existingTag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetById(Guid id)
        {
            return await dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<(bool isUpdated, Tag? Tag)> UpdateAsync(Tag tag)
        {
            var currentTag = await dbContext.Tags
               .Include(t => t.ArticleTags)
                .FirstOrDefaultAsync(x => x.Id == tag.Id);

            if (currentTag == null)
            {
                return (false, null);
            }

            var existingTag  = await dbContext.Tags
                  .FirstOrDefaultAsync(t => t.Name.ToLower() == tag.Name.ToLower());

            if (existingTag != null)
            {
                return (false, existingTag);
            }

            dbContext.Entry(currentTag).CurrentValues.SetValues(tag);
            await dbContext.SaveChangesAsync();
            return (true, tag);
        }
    }
}
