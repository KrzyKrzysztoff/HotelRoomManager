using HotelRoomManager.Infrastructure.Context;
using HotelRoomManager.Infrastructure.Data;
using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Domain.Models;
using HotelRoomManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelRoomManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == "Development" || environment == "Test")
            {
                services.AddDbContext<HotelRoomDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("HotelRoomManagerConnection");
                services.AddDbContext<HotelRoomDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }

            services.AddTransient<ISeedData, SeedData>();
            services.AddTransient<IRoomRepository, RoomRepository>();

            return services;
        }
    }
}