using BeautySalon.AuthandClient.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();



app.Run();
