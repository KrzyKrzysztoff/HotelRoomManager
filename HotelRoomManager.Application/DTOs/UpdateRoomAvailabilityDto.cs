using HotelRoomManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Application.DTOs
{
    public class UpdateRoomAvailabilityDto
    {
        public RoomStatus Status { get; set; }
        public AvailabilityDetailDto? Detail { get; set; }
    }
}
