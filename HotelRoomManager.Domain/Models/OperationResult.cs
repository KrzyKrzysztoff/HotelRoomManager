﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Domain.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string? Errors { get; set; }
    }
}
