using Common;
using Microsoft.EntityFrameworkCore;
using BindingFlags = System.Reflection.BindingFlags;

namespace SampleAPI.Data.Repositories;

public class DogRepository : IDogRepository
{
    private readonly AppDbContext _dbContext;

    public DogRepository(AppDbContext dbContext)
        => _dbContext = dbContext;


    /// <summary>
    /// Find a dog by name
    /// </summary>
    /// <param name="name">dog name</param>
    /// <returns></returns>
    public async Task<Dog?> GetDog(string name)
    {
        return await _dbContext
            .Set<Dog>()
            .FirstOrDefaultAsync(d => d.Name == name);
    }

    public async Task<List<Dog>> GetDogs(DogFilter filter)
    {
        var query = _dbContext.Set<Dog>().AsQueryable();

        //Sorting
        if (!string.IsNullOrEmpty(filter?.Attribute))
        {
            var property = typeof(Dog).GetProperty(filter.Attribute,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property is not null)
            {
                var propertyName = property.Name;

                query = propertyName switch
                {
                    "Name" => filter.Order != "desc"
                        ? query.OrderBy(d => d.Name)
                        : query.OrderByDescending(d => d.Name),
                    "Color" => filter.Order != "desc"
                        ? query.OrderBy(d => d.Color)
                        : query.OrderByDescending(d => d.Color),
                    "TailLength" => filter.Order != "desc"
                        ? query.OrderBy(d => d.TailLength)
                        : query.OrderByDescending(d => d.TailLength),
                    "Weight" => filter.Order != "desc"
                        ? query.OrderBy(d => d.Weight)
                        : query.OrderByDescending(d => d.Weight),
                    _ => query
                };
            }
        }

        // Pagination
        var dogs = await query
            .Skip(((filter!.PageNumber - 1) * filter.PageSize))
            .Take(filter.PageSize)
            .ToListAsync();

        return dogs;
    }

    public async Task CreateDog(Dog dog)
    {
        await _dbContext.AddAsync(dog);
        await _dbContext.SaveChangesAsync();
    }
}