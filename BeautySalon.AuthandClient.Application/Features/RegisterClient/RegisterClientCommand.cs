using BeautySalon.AuthandClient.Application.DTO;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.RegisterClient;

public class RegisterClientCommand : IRequest<AuthResponseDto>
{
    public RegisterClientDto RegisterClientDto { get; }

    public RegisterClientCommand(RegisterClientDto dto)
    {
        RegisterClientDto = dto;
    }
}