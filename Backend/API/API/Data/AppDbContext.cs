using API.Model;
using API.Model.Items;
using API.Model.Location;
using API.Model.Shelf;
using API.Model.User;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Location> Room { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Shelfs> Shelfs { get; set; }
}