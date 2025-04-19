using FluentValidation;
using Stefanini.Application.City.Models.Request;

namespace Stefanini.Application.Validators
{
    public class CityRequestValidator : AbstractValidator<CityRequest>
    {
        public CityRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters.");

            RuleFor(x => x.UF)
                .NotEmpty().WithMessage("UF is required.")
                .Length(2).WithMessage("UF must be exactly 2 characters.")
                .Matches("^[A-Z]{2}$").WithMessage("UF must contain only uppercase letters (e.g., SP).");
        }
    }
}
