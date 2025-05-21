using BeautySalon.AuthandClient.Application.DTO;
using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.ClientFeatures;

public record GetClientByUserIdQuery(Guid UserId) : IRequest<ClientDto?>;
