using NETReact.Shared.DTOs.ProjectDtos;

namespace Backend.Services.ProjectService
{
    public interface IProjectServ
    {
        Task <CreateProjectDto?> CreateProjectAsync (CreateProjectDto dto);
        Task<List<GetProjectDto>> ListProjectsAsync();
        Task<bool>UpdateProjectAsync(int  projectId, CreateProjectDto dto);

    }
}
