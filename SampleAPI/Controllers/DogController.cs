using Common;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SampleAPI.Handlers;

namespace SampleAPI.Controllers;

[ApiController]
[EnableRateLimiting("fixed")]
[Route("")]
public class DogController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Gets a list of dogs with optional sorting and pagination.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("dogs")]
    public async Task<IActionResult> GetDogs([FromQuery] DogFilter filter)
    {
        var dogs = await _mediator.Send(new GetDogsQuery { Filter = filter });
        return Ok(dogs);
    }

    /// <summary>
    /// Creates a dog by passing a JSON object in the request body.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="validator"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("dog")]
    public async Task<IActionResult> CreateDog([FromBody] DogCreationRequest request,
        [FromServices] IValidator<DogCreationRequest> validator)
    {
        await validator.ValidateAndThrowAsync(request);
        await _mediator.Send(new CreateDogCommand { CreationRequest = request });
        return Ok("Dog created");
    }
}