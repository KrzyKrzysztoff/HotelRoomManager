using HotelRoomManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.AddInfrastructure(configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();

