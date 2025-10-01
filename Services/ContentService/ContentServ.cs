using Backend.Data;
using Microsoft.EntityFrameworkCore;
using NETReact.Shared.DTOs.ContentDto;
using NETReact.Shared.Models;

namespace Backend.Services.ContentService
{
    public class ContentServ : IContentServ
    {
        private readonly DataContext _ctx;

        public ContentServ(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<CreateContentDto> CreateContentAsync(CreateContentDto contentDto)
        {
            var content = new Content
            {
                Name = contentDto.Name,
            };

            foreach(var item in contentDto.Items)
            {
                var contentItem = new Item
                {
                    Header = item.Header,
                    TextContent = item.TextContent,
                };
                content.Items.Add(contentItem);
            };

            _ctx.Contents.Add(content);
            await _ctx.SaveChangesAsync();

            return new CreateContentDto
            {
                Name = contentDto.Name,
                Items = contentDto.Items.Select(i => new CreateItemDto
                {
                    Header = i.Header,
                    TextContent = i.TextContent,
                }).ToList()
            };
            
            
        }

        public async Task<List<GetContentDto>> GetContentAsync()
        {
            var content = await _ctx.Contents
                .Include(c => c.Items)
                .Select(c => new GetContentDto
                {
                    Name = c.Name,
                    Items = c.Items.Select(i => new CreateItemDto
                    {
                        Header = i.Header,
                        TextContent = i.TextContent,
                    }).ToList(),
                }).ToListAsync();

            return content;
        }
    }
}
