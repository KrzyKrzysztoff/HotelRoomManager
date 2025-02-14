using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.Exceptions;

namespace HotelRoomManager.Application.Services
{
    public class RoomService(IRoomRepository roomRepository, IMapper mapper) : IRoomService
    {
        public async Task<IEnumerable<RoomDto>> GetAllSortedAsync(RoomSortBy sortBy)
        {
            try
            {
                var rooms = await roomRepository.GetAllAsync();

                rooms = sortBy switch
                {
                    RoomSortBy.NameAsc => rooms.OrderBy(r => r.Name),
                    RoomSortBy.NameDesc => rooms.OrderByDescending(r => r.Name),
                    RoomSortBy.SizeAsc => rooms.OrderBy(r => r.Size),
                    RoomSortBy.SizeDesc => rooms.OrderByDescending(r => r.Size),
                    RoomSortBy.Availability => rooms.OrderBy(r => r.Status),
                    _ => rooms
                };

                return mapper.Map<IEnumerable<RoomDto>>(rooms);

            }
            catch (Exception ex)
            {
                throw new RoomServiceException("Failed to get rooms", ex);
            }
        }

        public async Task<RoomDto?> GetRoomByIdAsync(Guid id)
        {
            try
            {
                var room = await roomRepository.GetByIdAsync(id);
                return mapper.Map<RoomDto>(room);
            }
            catch (Exception ex)
            {
                throw new RoomServiceException("Failed to get room by ID", ex);
            }
        }

        public async Task AddRoomAsync(RoomDto roomDto)
        {
            try
            {
                var room = mapper.Map<Room>(roomDto);
                await roomRepository.AddAsync(room);
            }
            catch (Exception ex)
            {
                throw new RoomServiceException("Failed to add room", ex);
            }
        }

        public async Task UpdateRoomAsync(RoomDto roomDto)
        {
            try
            {
                var room = mapper.Map<Room>(roomDto);
                await roomRepository.UpdateAsync(room);
            }
            catch (Exception ex)
            {
                throw new RoomServiceException("Failed to update room", ex);
            }
        }

        public async Task UpdateRoomAvailabilityAsync(Guid id, RoomStatus status, AvailabilityDetailDto? detailDto = null)
        {
            try
            {
                var detail = mapper.Map<AvailabilityDetail>(detailDto);
                await roomRepository.UpdateRoomAvailabilityAsync(id, status, detail);
            }
            catch (Exception ex)
            {
                throw new RoomServiceException("Failed to update room availability", ex);
            }

        }
    }
}
