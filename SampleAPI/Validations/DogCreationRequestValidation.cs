using Common;
using FluentValidation;

namespace SampleAPI.Validations;

public class DogCreationRequestValidation : AbstractValidator<DogCreationRequest>
{
    public DogCreationRequestValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.Color)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.TailLength)
            .NotEmpty()
            .InclusiveBetween(1, 200);
        
        RuleFor(x => x.Weight)
            .NotEmpty()
            .InclusiveBetween(1, 200);
    }    
}