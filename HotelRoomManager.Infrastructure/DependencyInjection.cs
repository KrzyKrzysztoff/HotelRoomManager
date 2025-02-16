using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Infrastructure.Context;
using HotelRoomManager.Infrastructure.Data;
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
            var connectionString = configuration.GetConnectionString("HotelRoomManagerConnection");

            services.AddDbContext<HotelRoomDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddTransient<ISeedData, SeedData>();
            services.AddTransient<IRoomRepository, RoomRepository>();

            return services;
        }
    }
}
