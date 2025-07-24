using Microsoft.EntityFrameworkCore;

namespace LogBg3Armory.Models;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<Rarity> Rarities { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Act> Acts { get; set; }
    public DbSet<Property> Properties { get; set; }
}