using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRoomManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomManager.Infrastructure.Context
{
    public class HotelRoomDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Room> Rooms{ get; set; }
    }
}
