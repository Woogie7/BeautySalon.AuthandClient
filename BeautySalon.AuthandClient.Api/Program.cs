using System.Text;
using BeautySalon.AuthandClient;
using BeautySalon.AuthandClient.Application;
using BeautySalon.AuthandClient.Persistence;
using BeautySalon.AuthandClient.Infrastructure;
using BeautySalon.AuthandClient.Infrastructure.Auht;
using BeautySalon.AuthandClient.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

app.Run();
