using BeautySalon.AuthandClient.Application.Interfaces;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.ClientFeatures.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetByIdAsync(request.ClientId);
        if (client == null)
            throw new Exception("Client not found");
        
        client.FullName = request.FullName;
        client.Phone = request.Phone;

        await _clientRepository.UpdateAsync(client);
    }
}
