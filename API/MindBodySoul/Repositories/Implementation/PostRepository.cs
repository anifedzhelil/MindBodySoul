using Microsoft.EntityFrameworkCore;
using MindBodySoul.Data;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO.Post;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Repositories.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Post> CreateAsync(Post post)
        {
            post.CreatedDate = DateTime.UtcNow;
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            return await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post?> UpdateAsync(Post post)
        {
            var existingPost = await dbContext.Posts.FirstOrDefaultAsync(x => x.Id == post.Id);
            if (existingPost == null) return null;

            existingPost.PostName = post.PostName;
            existingPost.IsPublished = post.IsPublished;
            existingPost.UpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
            return existingPost;
        }

        
        public async Task<IEnumerable<PostDto>> GetAllAsync(string? search = null)
        {
            var query = dbContext.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => EF.Functions.ILike(p.PostName, $"%{search}%"));
            }

            return await query
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    PostName = p.PostName,
                    UpdatedDate = p.UpdatedDate ?? p.CreatedDate,
                    CarouselCounts = p.CarouselDrafts != null ? p.CarouselDrafts.Count() : 0,
                    ThumbnailImage = p.Thumbnail != null ? p.Thumbnail.Image : ""
                })
                .ToListAsync();
        }
               
    }
}
