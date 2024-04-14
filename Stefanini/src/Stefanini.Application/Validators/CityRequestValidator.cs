using FluentValidation;
using Stefanini.Application.City.Models.Request;

namespace Stefanini.Application.Validators
{
    public class CityRequestValidator : AbstractValidator<CityRequest>
    {
        public CityRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required.");

            RuleFor(x => x.UF)
                .NotEmpty()
                .NotNull()
                .WithMessage("UF is required.");
        }
    }
}
