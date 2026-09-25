using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindBodySoul.Models.Domain;
using MindBodySoul.Models.DTO.CarouselDraft;
using MindBodySoul.Models.DTO.Post;
using MindBodySoul.Repositories.Interface;

namespace MindBodySoul.Controllers
{
    [Route("api/carouseldraft")]

    public class CarouselController: Controller
    {
        private readonly ICarouselRepository carouselRepository;
        private readonly ICarouselBulletRepository carouselBulletRepository; 

        public CarouselController(ICarouselRepository carouselRepository,
            ICarouselBulletRepository carouselBulletRepository)
        {
            this.carouselRepository = carouselRepository;
            this.carouselBulletRepository = carouselBulletRepository;
        }

        [HttpGet]
        [Route("{postId:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetCarouselsByPostId([FromRoute] Guid postId)
        {
            var carousels = await carouselRepository.GetOrderedIdsByPostIdAsync(postId);

            var response = new List<CarouselDraftSummaryDto>();

            foreach (var carousel in carousels)
            {
                response.Add(new CarouselDraftSummaryDto
                {
                    Id = carousel.Id,
                    Order = carousel.Order,
                });
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCarouselDraft([FromBody] CreateCarouselRequestDto request)
        {

            var carousel = new Carousel
            {
               Description = request.Description,
               Order = request.Order,
               Note =  request.Note,
               NoteSize = request.NoteSize,
               PaddingSide = request.PaddingSide,
               PaddingTop = request.PaddingTop, 
               BulletsSize = request.BulletsSize,
               DescriptionSize = request.DescriptionSize,
               NotePaddingTop = request.NotePaddingTop,
            };

            var response = await carouselRepository.CreateAsync(carousel);

            var bullets = request.Bullets.Select( bullet => new CarouselBullet
            {
                CarouselDraftId = response.Id,
                Header  =  bullet.Header,
                Order =  bullet.Order,
                Text = bullet.Text,
            }).ToList();

            object value = await carouselBulletRepository.AddRangeAsync(bullets);

            return Ok(new { response.Id });
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> UpdateCarouselDraft([FromRoute] Guid id, [FromBody] UpdateCarouselRequestDto request)
        {
            var carousel = await carouselRepository.GetByIdAsync(id);
            if (carousel == null)
            {
                return NotFound();
            }
            carousel.Description = request.Description;
            carousel.Order = request.Order;
            carousel.Note = request.Note;
            carousel.NoteSize = request.NoteSize;
            carousel.PaddingSide = request.PaddingSide;
            carousel.PaddingTop = request.PaddingTop;
            carousel.BulletsSize = request.BulletsSize;
            carousel.DescriptionSize = request.DescriptionSize;
            carousel.NotePaddingTop = request.NotePaddingTop;
            await carouselRepository.UpdateAsync(carousel);
            return Ok(carousel);
        }
        
    }
}
