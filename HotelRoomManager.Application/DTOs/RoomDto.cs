using HotelRoomManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Application.DTOs
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Size { get; set; }
        public RoomStatus Status { get; set; }
        public string? Description { get; set; }
        public AvailabilityDetailDto? Detail { get; set; }
    }
}
