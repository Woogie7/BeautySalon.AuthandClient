using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Interfaces;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.RegisterClient;

public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, AuthResponseDto>
{
    private readonly IAuthService _authService;

    public RegisterClientCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterClientAsync(request.RegisterClientDto);
    }   
}