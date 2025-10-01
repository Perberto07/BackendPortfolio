using Backend.Data;
using Microsoft.EntityFrameworkCore;
using NETReact.Shared.DTOs.ProjectDtos;
using NETReact.Shared.Models;

namespace Backend.Services.ProjectService
{
    public class ProjectServ : IProjectServ
    {
        private readonly DataContext _context;

        public ProjectServ(DataContext context)
        {
            _context = context;
        }

        public async Task<CreateProjectDto?> CreateProjectAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                Videolink = dto.VideoLink,

            };

            foreach(var tech in dto.TechStacks)
            {
                var techStack = new TechStack
                {
                    Name = tech.Name,
                    Description = tech.Description,
                };
                project.TechStacks.Add(techStack);
            }

            _context.Projects.Add(project); 
            await _context.SaveChangesAsync();

            return new CreateProjectDto
            {
                Title = project.Title,
                Description = project.Description,
                VideoLink = project.Videolink,
                TechStacks = project.TechStacks.Select(t => new TeckStackDto
                {
                    Name = t.Name,
                    Description = t.Description,
                }).ToList()
            };
        }

        public async Task<List<GetProjectDto>> ListProjectsAsync()
        {
            var project = await _context.Projects
                .Include(p => p.TechStacks)
                .Select(p => new GetProjectDto
                {
                    Title = p.Title,
                    Description = p.Description,
                    VideoLink= p.Videolink,
                    TechStacks = p.TechStacks.Select(s => new TeckStackDto
                    { 
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                    }).ToList(),
                }).ToListAsync();

            return project;
        }

        public async Task<bool> UpdateProjectAsync(int projectId, CreateProjectDto dto)
        {
            var project = await _context.Projects
                .Include(p => p.TechStacks)
                .Where(p => p.IsDelete != true)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
                throw new Exception("Project not found");

            project.Title = dto.Title;
            project.Description = dto.Description;
            project.Videolink = dto.VideoLink;

            foreach(var t in dto.TechStacks)
            {
                var techStack = project.TechStacks.FirstOrDefault(tech => tech.Id == t.Id);
                if(techStack != null)
                {
                    techStack.Name = t.Name;
                    techStack.Description = t.Description;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
