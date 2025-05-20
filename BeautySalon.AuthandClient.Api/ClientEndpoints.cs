using System.Security.Claims;

namespace BeautySalon.AuthandClient;

public static class ClientEndpoints
{
    public static IEndpointRouteBuilder MapClientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/client")
            .RequireAuthorization();

        group.MapGet("/me", (ClaimsPrincipal user) =>
        {
            var userId = user.FindFirst("userId")?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            return Results.Ok(new { userId, email });
        });

        return app;
    }
}
