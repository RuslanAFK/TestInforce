using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Url> Urls { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userRole = new Role
        {
            Id = 1,
            RoleName = "User"
        };
        var adminRole = new Role
        {
            Id = 2,
            RoleName = "Admin"
        };
        modelBuilder.Entity<Role>()
            .HasData(userRole);
        modelBuilder.Entity<Role>()
            .HasData(adminRole);
    }
}