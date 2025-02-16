using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.Services;
using HotelRoomManager.Application.Validators.RoomValidator;
using HotelRoomManager.Application.Validators.RoomValidators;
using HotelRoomManager.Application.Profiles;

namespace HotelRoomManager.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRoomService, RoomService>();

            services.AddScoped<IValidator<RoomDto>, RoomDtoValidator>();
            services.AddScoped<IValidator<UpdateRoomAvailabilityDto>, UpdateRoomAvailabilityDtoValidator>();
            services.AddScoped<IValidator<AvailabilityDetailDto>, AvailabilityDetailDtoValidator>();

            services.AddAutoMapper(typeof(RoomProfile));
        }
    }
}
