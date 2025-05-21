using MediatR;

namespace BeautySalon.AuthandClient.Application.Features.ClientFeatures;

public record UpdateClientCommand(Guid ClientId, string FullName, string Phone) : IRequest;
