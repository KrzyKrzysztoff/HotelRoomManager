﻿using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.Exceptions;
using HotelRoomManager.Application.Services;
using HotelRoomManager.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelRoomManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController(IRoomService roomService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRooms([FromQuery] RoomSortBy sortBy = RoomSortBy.NameAsc)
        {
            try
            {
                var rooms = await roomService.GetAllSortedAsync(sortBy);
                return Ok(rooms);
            }
            catch (RoomServiceException ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRoomById(Guid id)
        {
            try
            {
                var room = await roomService.GetRoomByIdAsync(id);
                if (room == null)
                {
                    return NotFound();
                }
                return Ok(room);
            }
            catch (RoomServiceException ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] RoomDto roomDto)
        {
            try
            {
               var result =  await roomService.AddRoomAsync(roomDto);

                if (!result.IsSuccess && !string.IsNullOrEmpty(result.Errors))
                    return BadRequest(new { Message = "An unexpected error occurred.", Errors = result.Errors });

                return Created();
            }
            catch (RoomServiceException ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoom([FromBody] RoomDto roomDto)
        {
            try
            {
               var result = await roomService.UpdateRoomAsync(roomDto);

                if (!result.IsSuccess && !string.IsNullOrEmpty(result.Errors))
                    return BadRequest(new { Message = "An unexpected error occurred.", Errors = result.Errors });

                return NoContent();
            }
            catch (RoomServiceException ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPatch("availability/")]
        public async Task<IActionResult> UpdateRoomAvailability([FromBody] UpdateRoomAvailabilityDto request)
        {
            try
            {
                var result = await roomService.UpdateRoomAvailabilityAsync(request);

                if (!result.IsSuccess && !string.IsNullOrEmpty(result.Errors))
                    return BadRequest(new { Message = "An unexpected error occurred.", Errors = result.Errors });


                return NoContent();
            }
            catch (RoomServiceException ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
