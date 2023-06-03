using Common;
using FluentValidation;
using SampleAPI.Validations;

namespace SampleAPI.Tests;

public class DogValidationTest
{
    
    [Theory]
    [InlineData("Test", "Test", -1, 1)]
    [InlineData("Test", "Test", 1, -1)]
    [InlineData("", "Test", 1, 1)]
    [InlineData("Test", "", 1, 1)]
    [InlineData("Test", "Test", 1, 201)]
    [InlineData("Test", "Test", 201, 1)]
    public void CreateDog_InvalidRequest_ThrowsException(string name, string color, int tailLength, int weight)
    {
        // Arrange
        var request = new DogCreationRequest(name, color, tailLength, weight);
        var validator = new DogCreationRequestValidation();

        //Act & Assert
        Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(request));
    }

    [Fact]
    public void CreateDog_DogCreationRequestIsValid_DoesNotThrowException()
    {
        // Arrange
        var request = new DogCreationRequest("Test", "Test", 1, 1);
        var validator = new DogCreationRequestValidation();

        //Act 
        var exception = Record.Exception(() => validator.ValidateAndThrow(request));

        //Assert
        Assert.Null(exception);
    }
}