using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Domain.Models;

namespace HotelRoomManager.Application.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Detail, opt => opt.MapFrom(src => src.Detail)); 

            CreateMap<AvailabilityDetail, AvailabilityDetailDto>();
        }
    }
}
