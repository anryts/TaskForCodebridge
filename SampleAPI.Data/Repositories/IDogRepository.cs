using Common;

namespace SampleAPI.Data.Repositories;

public interface IDogRepository
{
    /// <summary>
    /// Find a dog by name
    /// </summary>
    /// <param name="name">dog name</param>
    /// <returns></returns>
    Task<Dog?> GetDog(string name);
    Task<List<Dog>> GetDogs(DogFilter filter);
    Task CreateDog(Dog dog);
}