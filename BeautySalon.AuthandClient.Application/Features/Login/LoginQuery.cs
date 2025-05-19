using BeautySalon.AuthandClient.Application.DTO;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.Login;

public class LoginQuery : IRequest<AuthResponseDto>
{
    public string Email { get; }
    public string Password { get; }

    public LoginQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }
}