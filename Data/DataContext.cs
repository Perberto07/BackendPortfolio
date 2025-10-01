
using Microsoft.EntityFrameworkCore;
using NETReact.Shared.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<TechStack> TechStacks { get; set; } = default!;
        public DbSet<Content> Contents { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;

    }
}
