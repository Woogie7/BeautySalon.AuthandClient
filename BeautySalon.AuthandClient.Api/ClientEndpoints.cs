using System.Security.Claims;
using BeautySalon.AuthandClient.Application.Features.ClientFeatures;
using BeautySalon.AuthandClient.Application.Features.ClientFeatures.GetClientByUserId;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BeautySalon.AuthandClient;

public static class ClientEndpoints
{
    public static IEndpointRouteBuilder MapClientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/client")
            .RequireAuthorization();

        group.MapGet("/me", async (ClaimsPrincipal user, ISender sender) =>
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var clientDto = await sender.Send(new GetClientByUserIdQuery(userId));
            if (clientDto == null) return Results.NotFound();

            return Results.Ok(clientDto);
        });

        group.MapPut("/me", async (ClaimsPrincipal user, UpdateClientDto  dto, ISender sender) =>
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var clientDto = await sender.Send(new GetClientByUserIdQuery(userId));
            if (clientDto == null) return Results.NotFound();

            await sender.Send(new UpdateClientCommand(clientDto.Id, dto.FullName, dto.Phone));
            return Results.NoContent();
        });


        return app;
    }
}

public class UpdateClientDto
{
    public string FullName { get; set; }
    public string Phone { get; set; }
}
