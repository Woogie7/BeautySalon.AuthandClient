using BeautySalon.AuthandClient.Application.Features.Login;
using FluentValidation;

namespace BeautySalon.AuthandClient.Application.Validators;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}