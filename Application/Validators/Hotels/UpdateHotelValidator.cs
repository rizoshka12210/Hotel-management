using Application.DTOs.Hotels;
using FluentValidation;

namespace Application.Validators.Hotels;

public class UpdateHotelValidator : AbstractValidator<UpdateHotelDto>
{
    public UpdateHotelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Rating)
            .InclusiveBetween(0, 5);
    }
}