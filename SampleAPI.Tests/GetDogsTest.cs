using SampleAPI.Data;
using SampleAPI.Data.Repositories;
using Common;
using SampleAPI.Tests.Fixtures;

namespace SampleAPI.Tests;

public class GetDogsTest : IClassFixture<DbContextFixture>
{
    private readonly AppDbContext _dbContext;

    public GetDogsTest(DbContextFixture fixture)
        => _dbContext = fixture.AppDbContext;


    [Fact]
    public async Task GetDogs_WithValidFilter_ReturnsDogsOrderedByNameAscending()
    {
        // Arrange
        var dogRepository = new DogRepository(_dbContext);
        var filter = new DogFilter(Attribute: "Name", Order: "", PageNumber: 1, PageSize: 10);

        // Act
        var result = await dogRepository.GetDogs(filter);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal("Bailey", result[0].Name);
        Assert.Equal("Bella", result[1].Name);
        Assert.Equal("Charlie", result[2].Name);
    }

    [Fact]
    public async Task GetDogs_WithValidFilter_ReturnsDogsOrderedByNameDescending()
    {
        // Arrange
        var dogRepository = new DogRepository(_dbContext);

        var filter = new DogFilter(Attribute: "Name", Order: "desc", PageNumber: 1, PageSize: 10);

        // Act
        var result = await dogRepository.GetDogs(filter);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal("Charlie", result[0].Name);
        Assert.Equal("Bella", result[1].Name);
        Assert.Equal("Bailey", result[2].Name);
    }


    [Fact]
    public async Task GetDogs_WithPaging_ReturnsCorrectPageOfDogs()
    {
        // Arrange
        var dogRepository = new DogRepository(_dbContext);

        var filter = new DogFilter(Attribute: "Name", Order: "", PageNumber: 2, PageSize: 2);

        // Act
        var result = await dogRepository.GetDogs(filter);

        // Assert
        Assert.Single(result);
        Assert.Equal("Charlie", result[0].Name);
    }
}