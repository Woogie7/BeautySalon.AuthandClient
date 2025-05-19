using BeautySalon.AuthandClient;
using BeautySalon.AuthandClient.Application;
using BeautySalon.AuthandClient.Persistence;
using BeautySalon.AuthandClient.Infrastructure;
using BeautySalon.AuthandClient.Infrastructure.Auht;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapAuthEndpoints();

app.Run();
