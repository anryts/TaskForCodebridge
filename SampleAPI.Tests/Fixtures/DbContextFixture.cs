using Microsoft.EntityFrameworkCore;
using SampleAPI.Data;

namespace SampleAPI.Tests.Fixtures;

public class DbContextFixture : IDisposable
{
    public AppDbContext AppDbContext { get; private set; }

    public DbContextFixture()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        List<Dog> dogs = new()
        {
            new Dog { Name = "Bella", Color = "Brown", TailLength = 10, Weight = 125 },
            new Dog { Name = "Charlie", Color = "Black", TailLength = 8, Weight = 152 },
            new Dog { Name = "Bailey", Color = "White", TailLength = 12, Weight = 97 }
        };
        AppDbContext = new AppDbContext(options);
        AppDbContext.Set<Dog>().AddRange(dogs);
        AppDbContext.SaveChanges();
    }

    public void Dispose()
    {
        AppDbContext.Dispose();
    }
}