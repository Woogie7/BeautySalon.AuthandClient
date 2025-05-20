using System.Security.Claims;
using System.Text;
using BeautySalon.AuthandClient;
using BeautySalon.AuthandClient.Application;
using BeautySalon.AuthandClient.Persistence;
using BeautySalon.AuthandClient.Infrastructure;
using BeautySalon.AuthandClient.Infrastructure.Auht;
using BeautySalon.AuthandClient.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()!;
if (string.IsNullOrEmpty(jwtOptions?.SecretKey))
{
    throw new InvalidOperationException("JWT SecretKey is not configured.");
}
var key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.MigrateDbAsync();

app.UseExceptionHandling();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();
app.MapClientEndpoints();

app.MapGet("/auth/me", (HttpContext context) =>
    {
        var user = context.User;

        if (!user.Identity?.IsAuthenticated ?? false)
            return Results.Unauthorized();

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        var role = user.FindFirst(ClaimTypes.Role)?.Value;
        var email = user.FindFirst(ClaimTypes.Email)?.Value;

        return Results.Ok(new
        {
            UserId = userId,
            Email = email,
            Role = role
        });
    })
    .RequireAuthorization();


app.Run();
