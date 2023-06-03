using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SampleAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base (options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}