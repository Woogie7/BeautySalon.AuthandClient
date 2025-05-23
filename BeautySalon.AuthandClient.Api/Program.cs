using System.Security.Claims;
using System.Text;
using BeautySalon.AuthandClient;
using BeautySalon.AuthandClient.Application;
using BeautySalon.AuthandClient.Persistence;
using BeautySalon.AuthandClient.Infrastructure;
using BeautySalon.AuthandClient.Infrastructure.Auht;
using BeautySalon.AuthandClient.Middleware;
using BeautySalon.Booking.Infrastructure.Rabbitmq;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()  
    .CreateLogger();

builder.Host.UseSerilog();

var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()!;

Log.Logger.Information("JwtOptions in Employees Service: SecretKey = {SecretKey}, Issuer = {Issuer}, Audience = {Audience}",
    jwtOptions.SecretKey, jwtOptions.Issuer, jwtOptions.Audience);

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
        
    options.AddPolicy("EmployeeOnly", policy =>
        policy.RequireRole("Employee"));
        
    options.AddPolicy("ClientOnly", policy =>
        policy.RequireRole("Client"));
});

builder.Services.AddMassTransit(busConfing =>
{
    busConfing.SetKebabCaseEndpointNameFormatter();

    busConfing.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri("amqp://rabbitmq:5672"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection("MessageBroker"));

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

app.MapGet("/me", (HttpContext context) =>
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
