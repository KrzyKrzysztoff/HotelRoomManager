﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Domain.Models
{
    public class AvailabilityDetail
    {
        public Guid Id { get; set; }
        public string Reason { get; set; } = null!;
        public string? Description { get; set; }
    }
}
