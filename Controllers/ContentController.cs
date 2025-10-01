using Backend.Services.ContentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETReact.Shared.DTOs.ContentDto;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentServ _item;

        public ContentController(IContentServ item)
        {
            _item = item;
        }

        [HttpPost("create-content")]
        public async Task<IActionResult> CreateContent(CreateContentDto dto)
        {
            var result = await _item.CreateContentAsync(dto);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet("get-content")]
        public async Task<IActionResult> GetProject()
        {
            var content = await _item.GetContentAsync();

            if (content != null) 
            {
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
