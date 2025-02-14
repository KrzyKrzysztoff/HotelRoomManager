using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Domain.Models;

namespace HotelRoomManager.Application.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllSortedAsync(RoomSortBy sortBy);
        Task<RoomDto?> GetRoomByIdAsync(Guid id);
        Task AddRoomAsync(RoomDto room);
        Task UpdateRoomAsync(RoomDto room);
        Task UpdateRoomAvailabilityAsync(Guid id, RoomStatus status, AvailabilityDetailDto? detail = null);
    }
}
