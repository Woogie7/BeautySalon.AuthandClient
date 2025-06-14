using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Application.Features.Login;
using BeautySalon.AuthandClient.Application.Features.RegisterClient;
using BeautySalon.AuthandClient.Domain;
using FluentValidation;
using MediatR;

namespace BeautySalon.AuthandClient;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/register/client", async (
            RegisterUserDto dto,
            IValidator<RegisterUserDto> validator,
            ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            dto.Role = null;
            var result = await sender.Send(new RegisterClientCommand(dto));
            return Results.Ok(result);
        });

        group.MapPost("/register/employee", async (
            HttpContext context,
            RegisterUserDto dto,
            IValidator<RegisterUserDto> validator,
            ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            if (!context.User.IsInRole("Admin"))
                return Results.Forbid();

            dto.Role = Enumeration.FromDisplayName<UserRole>("Employee").ToString();
            var result = await sender.Send(new RegisterClientCommand(dto));
            return Results.Ok(result);
        }).RequireAuthorization("AdminOnly");

        group.MapPost("/login", async (
            LoginQuery query,
            IValidator<LoginQuery> validator,
            ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var result = await sender.Send(query);
            return Results.Ok(result);
        });
        
        return app;
    }
}