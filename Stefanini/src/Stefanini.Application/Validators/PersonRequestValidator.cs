using FluentValidation;
using Stefanini.Application.Person.Models.Request;

namespace Stefanini.Application.Validators;

public class PersonRequestValidator : AbstractValidator<PersonRequest>
{
    public PersonRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.CPF)
            .NotEmpty()
            .WithMessage("CPF is required.");

        RuleFor(x => x.Age)
            .NotEmpty()
            .WithMessage("Age is required.");
}
}
