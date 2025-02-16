using HotelRoomManager.Application;
using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Infrastructure;
using HotelRoomManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seedData = services.GetRequiredService<ISeedData>();

    await seedData.Initialize();
}
app.UseHttpsRedirection();

app.Run();

