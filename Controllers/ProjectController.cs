using Backend.Services.ProjectService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETReact.Shared.DTOs.ProjectDtos;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectServ _project;

        public ProjectController(IProjectServ project)
        {
            _project = project;
        }

        [HttpPost("create-project")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var result = await _project.CreateProjectAsync(dto);
            return Ok(result);
        }

        [HttpGet("Get-Project")]
        public async Task<IActionResult> GetProject()
        {
            var project = await _project.ListProjectsAsync();
            return Ok(project);
        }

        [HttpPut("{projectId}/update")]
        public async Task<ActionResult> UpdateProject(int projectId, [FromBody] CreateProjectDto dto)
        {
            try
            {
                var success = await _project.UpdateProjectAsync(projectId, dto);
                if (!success)
                    return NotFound(new { message = "Project Not Found." });

                return Ok(new { message = "Porject Updated!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
