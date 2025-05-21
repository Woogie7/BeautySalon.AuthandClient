using BeautySalon.AuthandClient.Application.DTO;
using FluentValidation;

namespace BeautySalon.AuthandClient.Application.Validators;

public class RegisterClientValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterClientValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+?\d{10,15}$")
            .WithMessage("Invalid phone number format");
    }
}