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
using FluentValidation;
using HotelRoomManager.Application.Validators.RoomValidator;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using HotelRoomManager.Application.Validators.RoomValidators;

namespace HotelRoomManager.Application.Services
{
    public class RoomService(IRoomRepository roomRepository,
        IMapper mapper,
        IValidator<RoomDto> roomDtoValidator,
        IValidator<UpdateRoomAvailabilityDto> roomAvailabilityDtoValidator) : IRoomService
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
                throw new RoomServiceException($"Failed to get rooms: {ex.Message}", ex);
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
                throw new RoomServiceException($"Failed to get room by ID: {ex.Message}", ex);
            }
        }

        public async Task<OperationResult> AddRoomAsync(RoomDto roomDto)
        {
            var result = new OperationResult();

            try
            {
                var model = await roomDtoValidator.ValidateAsync(roomDto);

                if (!model.IsValid)
                {
                    result.IsSuccess = false;
                    result.Errors = string.Join(", ", model.Errors.Select(e => e.ErrorMessage));
                    return result; 
                }

                var room = mapper.Map<Room>(roomDto);

                await roomRepository.AddAsync(room);

                result.IsSuccess = true;
                result.Errors = string.Empty; 
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Errors = $"Failed to add room: {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> UpdateRoomAsync(RoomDto roomDto)
        {
            var result = new OperationResult();
            try
            {
                var model = await roomDtoValidator.ValidateAsync(roomDto);

                if (!model.IsValid)
                {
                    result.IsSuccess = false;
                    result.Errors = string.Join(", ", model.Errors.Select(e => e.ErrorMessage));
                    return result;
                }

                var room = mapper.Map<Room>(roomDto);
                await roomRepository.UpdateAsync(room);

                result.IsSuccess = true;
                result.Errors = string.Empty;
            }
            catch (Exception ex)
            {
                throw new RoomServiceException($"Failed to update room: {ex.Message}", ex);
            }

            return result;
        }

        public async Task<OperationResult> UpdateRoomAvailabilityAsync(UpdateRoomAvailabilityDto updateRoomAvailabilityDto)
        {
            var result = new OperationResult();
            try
            {
                var model = await roomAvailabilityDtoValidator.ValidateAsync(updateRoomAvailabilityDto);

                if (!model.IsValid)
                {
                    result.IsSuccess = false;
                    result.Errors = string.Join(", ", model.Errors.Select(e => e.ErrorMessage));
                    return result;
                }

                var detail = mapper.Map<AvailabilityDetail>(updateRoomAvailabilityDto.Detail);
                await roomRepository.UpdateRoomAvailabilityAsync(updateRoomAvailabilityDto.RoomId, updateRoomAvailabilityDto.Status, detail);
            }
            catch (Exception ex)  
            {
                throw new RoomServiceException($"Failed to update room availability: {ex.Message}", ex);
            }

            return result;
        }
    }
}
