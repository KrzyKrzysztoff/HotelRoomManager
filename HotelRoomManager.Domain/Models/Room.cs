using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Domain.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Size { get; set; }
        public RoomStatus Status { get; set; }
        public string? Description { get; set; }
        public AvailabilityDetail? Detail { get; set; }
    }
   
}
