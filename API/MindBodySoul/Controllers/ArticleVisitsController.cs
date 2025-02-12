using Microsoft.AspNetCore.Mvc;

namespace MindBodySoul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleVisitsController : Controller
    {
        public async Task<IActionResult> IncrementVisit(Guid id)
        {

            return Ok();
        }
    }
}
