using Microsoft.AspNetCore.Mvc;
using NewsApp.Core.Interfaces;
using NewsApp.Core.Models;

namespace NewsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly INewsService _newsService;

        public NewsController(ILogger<NewsController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<NewsItemModel>>> GetAll()
        {
            var response = await _newsService.GetAllStories().ConfigureAwait(false);
            return response != null ? Ok(response) : NotFound();
        }
    }
}
