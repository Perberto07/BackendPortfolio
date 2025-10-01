using NETReact.Shared.DTOs.NewsDto;

namespace Backend.Services.NewsService
{
    public interface INewsServ
    {
        public Task<List<NewsArticleDto>> GetNewsAsync();
    }
}
