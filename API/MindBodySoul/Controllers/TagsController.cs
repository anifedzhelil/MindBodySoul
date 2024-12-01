using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequestDto request)
        {
            var tag = new Tag
            {
                Name = request.Name
            };

            var result = await tagRepository.CreateAsync(tag);

            if (!result.isCreated)
            {
              return BadRequest($"Тагът '{result.Tag?.Name}' съществува!");
            } 

            var response = new TagDto { Id = result.Tag.Id, Name = result.Tag.Name };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await tagRepository.GetAllAsync();

            var response = new List<TagDto>();

            foreach (var tag in tags)
            {
                response.Add(new TagDto
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTagById([FromRoute] Guid id)
        {
            var tag = await tagRepository.GetById(id);

            if (tag == null)
            {
                return NotFound();
            }

            var response = new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditTag([FromRoute] Guid id, TagDto request)
        {
            var tag = new Tag
            {
                Id = id,
                Name = request.Name
            };

            var  result = await tagRepository.UpdateAsync(tag);

            if (result.Tag == null)
            {
                return NotFound();
            }

            if (!result.isUpdated)
            {
                return BadRequest($"Тагът '{result.Tag?.Name}' съществува!");

            }

            var response = new TagDto
            {
                Id = result.Tag.Id,
                Name = result.Tag.Name
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteTag([FromRoute] Guid id) 
        {
            var tag = await tagRepository.DeleteAsync(id);

            if(tag is null)
            {
                return NotFound();
            }

            if(tag.ArticleTags != null && tag.ArticleTags.Any()) 
            {
                return BadRequest("Тагът не може бъде изтрит. Съществуват постове с този таг");                                                                           
            }

            var response = new TagDto()
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return Ok(response);
        }
    }
}
