using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync(Guid articleId)
        {
            return await dbContext.Comments.Where(x=> x.ArticleId == articleId)
                .ToListAsync();
        }

        public async Task<Comment?> GetById(Guid Id)
        {
            return await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == Id);
        }
        public async Task<Comment?> UpdateAsync(Guid id, string content, DateTime updatedDate)
        {
            var existingComment = await dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingComment != null)
            {
                var updatedValues = new
                {
                    Content = content,
                    UpdatedDate = updatedDate
                };

                dbContext.Entry(existingComment).CurrentValues.SetValues(updatedValues);
                await dbContext.SaveChangesAsync();
                return existingComment;
            }

            return null;
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            var existingComment = await dbContext.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingComment is null)
            {
                return null;
            }

            dbContext.Comments.Remove(existingComment);
            await dbContext.SaveChangesAsync();

            return existingComment;
        }

        public async Task<List<Comment>> DeleteRangeAsync(Guid articleId)
        {
            var comments = await dbContext.Comments
               .Where(c => c.ArticleId == articleId)
               .ToListAsync();

            dbContext.Comments.RemoveRange(comments);

            await dbContext.SaveChangesAsync();

            return comments;
        }
    }
}
