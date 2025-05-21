using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Features.Login;
using BeautySalon.AuthandClient.Application.Features.RegisterClient;
using BeautySalon.AuthandClient.Domain;
using MediatR;

namespace BeautySalon.AuthandClient;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/register/client", async (RegisterUserDto dto, ISender sender) =>
        {
            dto.Role = null;
            var result = await sender.Send(new RegisterClientCommand(dto));
            return Results.Ok(result);
        });
        
        group.MapPost("/register/employee", async (HttpContext context, RegisterUserDto dto, ISender sender) =>
        {
            dto.Role = Enumeration.FromDisplayName<UserRole>("Employee").ToString();
            if (!context.User.IsInRole("Admin"))
                return Results.Forbid();

            var result = await sender.Send(new RegisterClientCommand(dto));
            return Results.Ok(result);
        }).RequireAuthorization("AdminOnly");

        group.MapPost("/login", async (LoginQuery query, ISender sender) =>
        {
            var result = await sender.Send(query);
            return Results.Ok(result);
        });

        return app;
    }
}