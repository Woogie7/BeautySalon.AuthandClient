using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Interfaces;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.ClientFeatures.GetClientByUserId;

public class GetClientByUserIdQueryHandler : IRequestHandler<GetClientByUserIdQuery, ClientDto?>
{
    private readonly IClientRepository _clientRepository;

    public GetClientByUserIdQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto?> Handle(GetClientByUserIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByUserIdAsync(request.UserId);
        if (client == null) return null;

        return new ClientDto
        {
            Id = client.Id,
            UserId = client.UserId,
            FullName = client.FullName,
            Phone = client.Phone
        };
    }
}
