using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRoomManager.Domain.Models;

namespace HotelRoomManager.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync(RoomSortBy sortBy);
        Task<Room?> GetByIdAsync(Guid id);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task UpdateRoomAvailabilityAsync(Guid id, RoomStatus status, AvailabilityDetail? detail = null);
    }
}
