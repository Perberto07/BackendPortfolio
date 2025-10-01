using NETReact.Shared.DTOs.ContentDto;
using NETReact.Shared.Models;

namespace Backend.Services.ContentService
{
    public interface IContentServ
    {
        public Task<CreateContentDto> CreateContentAsync (CreateContentDto contentDto);
        public Task<List<GetContentDto>> GetContentAsync();
        
    }
}
