using Common;
using MediatR;
using SampleAPI.Data;
using SampleAPI.Data.Repositories;

namespace SampleAPI.Handlers;

public class GetDogsQuery : IRequest<List<Dog>>
{
    public DogFilter Filter { get; init; }
}

public class GetDogsQueryHandler : IRequestHandler<GetDogsQuery, List<Dog>>
{
    private readonly IDogRepository _dogRepository;

    public GetDogsQueryHandler(IDogRepository dogRepository)
        => _dogRepository = dogRepository;


    public async Task<List<Dog>> Handle(GetDogsQuery request, CancellationToken cancellationToken)
    {
        return await _dogRepository.GetDogs(request.Filter);
    }
}