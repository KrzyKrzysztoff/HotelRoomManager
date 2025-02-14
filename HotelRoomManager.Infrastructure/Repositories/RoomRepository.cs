using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Domain.Models;
using HotelRoomManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomManager.Infrastructure.Repositories
{
    public class RoomRepository(HotelRoomDbContext dbContext) : IRoomRepository
    {
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await dbContext.Rooms.ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            return await dbContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Room room)
        {
            await dbContext.AddAsync(room);
            await dbContext.SaveChangesAsync(); 
        }

        public async Task UpdateAsync(Room room)
        {
            dbContext.Rooms.Update(room);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateRoomAvailabilityAsync(Guid id, RoomStatus status, AvailabilityDetail? detail = null)
        {
            var room = await GetByIdAsync(id);

            if (room == null)
                throw new InvalidOperationException($"Room with id {id} not found");

            room.Status = status;
            room.Detail = detail; 

            await dbContext.SaveChangesAsync();
        }
    }
}
