using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Domain.Models;

namespace HotelRoomManager.Application.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllSortedAsync(RoomSortBy sortBy);
        Task<RoomDto?> GetRoomByIdAsync(Guid id);
        Task<OperationResult> AddRoomAsync(RoomDto room);
        Task<OperationResult> UpdateRoomAsync(RoomDto roomDto);
        Task<OperationResult> UpdateRoomAvailabilityAsync(UpdateRoomAvailabilityDto updateRoomAvailabilityDto);
    }
}
