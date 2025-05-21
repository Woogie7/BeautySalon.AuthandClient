using BeautySalon.AuthandClient.Application.DTO;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.RegisterClient;

public class RegisterClientCommand : IRequest<AuthResponseDto>
{
    public RegisterUserDto RegisterUserDto { get; }

    public RegisterClientCommand(RegisterUserDto dto)
    {
        RegisterUserDto = dto;
    }
}