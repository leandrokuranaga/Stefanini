using FluentValidation;
using Stefanini.Application.Person.Models.Request;

namespace Stefanini.Application.Validators;

public class PersonRequestValidator : AbstractValidator<PersonRequest>
{
    public PersonRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be at most 100 characters.");

        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF is required.")
            .Length(11).WithMessage("CPF must be 11 characters.")
            .Matches(@"^\d{11}$").WithMessage("CPF must contain only numbers.");

        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Age is required.")
            .InclusiveBetween(0, 150).WithMessage("Age must be between 0 and 150.");

        RuleFor(x => x.CityId)
            .GreaterThan(0).When(x => x.CityId.HasValue)
            .WithMessage("CityId must be greater than 0 when provided.");
    }
}
