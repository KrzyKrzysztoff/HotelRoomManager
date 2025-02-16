using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Application.DTOs
{
    public class AvailabilityDetailDto
    {
        public string Reason { get; set; } = null!;
        public string? Description { get; set; }
    }
}
