using NETReact.Shared.DTOs.NewsDto;

namespace Backend.Services.NewsService
{
    public class NewsServ : INewsServ
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "pub_2bb3decfa10b46aeaa7146682244f381";
        private const string ApiUrl =
            "https://newsdata.io/api/1/latest?apikey=" + ApiKey +
            "&country=ph&language=tl,en&category=top,technology,politics,environment,crime&timezone=Asia/Taipei";
        public NewsServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NewsArticleDto>> GetNewsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<NewsApiResponseDto>(ApiUrl);

            return response?.Results?.Select(r => new NewsArticleDto
            {
                Title = r.Title,
                Link = r.Link,
                Description = r.Description,
                ImageUrl = r.Image_Url,
                Creator = r.Creator?.FirstOrDefault() ?? "Unknown",
                PubDate = r.PubDate,
                SourceName = r.Source_Name,
                Country = r.Country,
                Category = r.Category
            }).ToList() ?? new List<NewsArticleDto>();
        }
    }
}
