using HotelRoomManager.Application;
using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Infrastructure;
using HotelRoomManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelRoomManager API v1");
    x.RoutePrefix = string.Empty;
});

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environment != "Test")
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var seedData = services.GetRequiredService<ISeedData>();

        await seedData.Initialize();
    }
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }