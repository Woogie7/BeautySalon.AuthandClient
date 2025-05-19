using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Interfaces;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponseDto>
{
    private readonly IAuthService _authService;

    public LoginQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request.Email, request.Password);
    }
}