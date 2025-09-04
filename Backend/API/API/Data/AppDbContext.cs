using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Item> Items { get; set; }
    public DbSet<Location> Room { get; set; }
    public DbSet<User> Users { get; set; }
}