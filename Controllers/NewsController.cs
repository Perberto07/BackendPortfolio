using Backend.Services.NewsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsServ _news;
        public NewsController(INewsServ news)
        {
            _news = news;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var articles = await _news.GetNewsAsync();

            if (articles == null || !articles.Any())
                return NotFound(new { message = "No news articles found." });

            return Ok(articles);
        }
    }
}
