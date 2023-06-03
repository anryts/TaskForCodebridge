using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SampleAPI.Data;

public class AddDbContext : DbContext
{
    public AddDbContext(DbContextOptions options) : base (options) 
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}