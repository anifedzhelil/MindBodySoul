using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO;
using MindBodySoul.Models.DTO.Post;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository postRepository; 
        public PostsController(IPostRepository postRepository)
        { 
            this.postRepository = postRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDto request)
        {
            var post = new Post
            {
                PostName = request.PostName,
            };

            var created = await postRepository.CreateAsync(post);

            var response = new PostDto
            {
                Id = created.Id,
                PostName = created.PostName,
                IsPublished = created.IsPublished,
                UpdatedDate = created.UpdatedDate ?? created.CreatedDate,
                CarouselCounts = 0,
                ThumbnailImage = ""
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllPosts")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> GetAllPosts([FromQuery] string? search)
        {
            var posts = await postRepository.GetAllAsync(search);
            var response = new List<PostDto>();


            foreach (var post in posts)
            {
                response.Add(new PostDto
                {
                    Id = post.Id,
                    PostName = post.PostName,
                    IsPublished = post.IsPublished,
                    UpdatedDate = post.UpdatedDate ?? post.CreatedDate,
                    CarouselCounts = post.Carousel != null ? post.Carousel.Count() : 0,
                    ThumbnailImage = post.Thumbnail != null ? post.Thumbnail.Image : "",
                });
             };
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdatePost(Guid id, UpdatePostRequestDto request)
        {
            var post = new Post
            {
                Id = id,
                PostName = request.PostName,
                IsPublished = request.IsPublished,
            };          

            post = await postRepository.UpdateAsync(post);

            if (post == null)
            {
                return NotFound();
            }
            var response = new PostDto()
            {
                Id = post.Id,
                PostName = post.PostName,
                IsPublished = post.IsPublished,
                UpdatedDate = post.UpdatedDate ?? post.CreatedDate,
            };

            return Ok(response);
        }

    }
}
