using Common;
using MediatR;
using SampleAPI.Data;
using SampleAPI.Data.Repositories;

namespace SampleAPI.Handlers;

public class CreateDogCommand : IRequest<Unit>
{
    public DogCreationRequest CreationRequest { get; init; }
}

public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, Unit>
{
    private readonly IDogRepository _dogRepository;

    public CreateDogCommandHandler(IDogRepository dogRepository)
        => _dogRepository = dogRepository;


    public async Task<Unit> Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        if(await _dogRepository.GetDog(request.CreationRequest.Name) is not null)
            throw new Exception("Dog already exists");
        
        await _dogRepository.CreateDog(new Dog
        {
            Name = request.CreationRequest.Name,
            Color = request.CreationRequest.Color,
            TailLength = request.CreationRequest.TailLength,
            Weight = request.CreationRequest.Weight,
        });

        return Unit.Value;
    }
}