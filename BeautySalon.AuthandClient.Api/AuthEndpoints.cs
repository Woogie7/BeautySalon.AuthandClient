using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Features.Login;
using BeautySalon.AuthandClient.Application.Features.RegisterClient;
using MediatR;

namespace BeautySalon.AuthandClient;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/register", async (RegisterClientDto dto, ISender sender) =>
        {
            var result = await sender.Send(new RegisterClientCommand(dto));
            return Results.Ok(result);
        });

        group.MapPost("/login", async (LoginQuery query, ISender sender) =>
        {
            var result = await sender.Send(query);
            return Results.Ok(result);
        });

        return app;
    }
}