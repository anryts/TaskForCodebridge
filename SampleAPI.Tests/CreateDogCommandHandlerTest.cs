using Common;
using Moq;
using SampleAPI.Data;
using SampleAPI.Data.Repositories;
using SampleAPI.Handlers;

namespace SampleAPI.Tests;

public class CreateDogCommandHandlerTest
{
    [Fact]
    public async Task Handle_WhenDogAlreadyExists_ThrowsException()
    {
        // Arrange
        var dogRepositoryMock = new Mock<IDogRepository>();
        dogRepositoryMock.Setup(x => x.GetDog(It.IsAny<string>()))
            .ReturnsAsync(new Dog());
        var handler = new CreateDogCommandHandler(dogRepositoryMock.Object);
        var command = new CreateDogCommand
        {
            CreationRequest = new DogCreationRequest("Test", "Test", 1, 1)
        };

        //Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenDogDoesNotExist_CallsCreateDog()
    {
        // Arrange
        var dogRepositoryMock = new Mock<IDogRepository>();
        dogRepositoryMock.Setup(x => x.GetDog(It.IsAny<string>()))
            .ReturnsAsync((Dog?)null);
        var handler = new CreateDogCommandHandler(dogRepositoryMock.Object);
        var command = new CreateDogCommand
        {
            CreationRequest = new DogCreationRequest("Test", "Test", 1, 1)
        };

        //Act
        await handler.Handle(command, CancellationToken.None);

        //Assert
        dogRepositoryMock.Verify(x => x.CreateDog(It.IsAny<Dog>()), Times.Once);
    }
}