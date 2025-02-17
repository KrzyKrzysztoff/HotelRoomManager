using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Application.Exceptions;
using HotelRoomManager.Application.Services;
using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Domain.Models;
using Moq;

namespace HotelRoomManager.Tests
{

    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> roomRepositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IValidator<RoomDto>> roomDtoValidatorMock;
        private readonly Mock<IValidator<UpdateRoomAvailabilityDto>> updateRoomAvailabilityValidatorMock;
        private readonly RoomService roomService;

        public RoomServiceTests()
        {
            roomRepositoryMock = new Mock<IRoomRepository>();
            mapperMock = new Mock<IMapper>();
            roomDtoValidatorMock = new Mock<IValidator<RoomDto>>();
            updateRoomAvailabilityValidatorMock = new Mock<IValidator<UpdateRoomAvailabilityDto>>();

            roomService = new RoomService(
                roomRepositoryMock.Object,
                mapperMock.Object,
                roomDtoValidatorMock.Object,
                updateRoomAvailabilityValidatorMock.Object
            );
        }

        [Fact]
        public async Task GetAllSortedAsync_NameAsc_ShouldSortByNameAscending()
        {
            // Arrange
            var rooms = new List<Room>
        {
            new Room { Name = "C" },
            new Room { Name = "A" },
            new Room { Name = "B" }
        };

            roomRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(rooms);
            mapperMock.Setup(m => m.Map<IEnumerable<RoomDto>>(It.IsAny<IEnumerable<Room>>()))
                      .Returns((IEnumerable<Room> r) => r.Select(x => new RoomDto { Name = x.Name }));

            // Act
            var result = await roomService.GetAllSortedAsync(RoomSortBy.NameAsc);

            // Assert
            Assert.Collection(result,
                item => Assert.Equal("A", item.Name),
                item => Assert.Equal("B", item.Name),
                item => Assert.Equal("C", item.Name)
            );
        }

        [Fact]
        public async Task GetAllSortedAsync_WhenExceptionThrown_ShouldThrowRoomServiceException()
        {
            // Arrange
            roomRepositoryMock.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<RoomServiceException>(() => roomService.GetAllSortedAsync(RoomSortBy.NameAsc));
            Assert.Equal("Failed to get rooms: Database error", exception.Message);
        }

        [Fact]
        public async Task GetRoomByIdAsync_ShouldReturnMappedRoomDto()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var room = new Room { Id = roomId, Name = "Conference Room" };
            var roomDto = new RoomDto { Id = roomId, Name = "Conference Room" };

            roomRepositoryMock.Setup(repo => repo.GetByIdAsync(roomId)).ReturnsAsync(room);
            mapperMock.Setup(m => m.Map<RoomDto>(room)).Returns(roomDto);

            // Act
            var result = await roomService.GetRoomByIdAsync(roomId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(roomId, result.Id);
            Assert.Equal("Conference Room", result.Name);
        }

        [Fact]
        public async Task AddRoomAsync_WhenValidationFails_ShouldReturnErrors()
        {
            // Arrange
            var roomDto = new RoomDto { Name = "" }; 
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ("Name", "Name is required.")
            });

            roomDtoValidatorMock.Setup(v => v.ValidateAsync(roomDto, default))
                .ReturnsAsync(validationResult);

            // Act
            var result = await roomService.AddRoomAsync(roomDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Name is required.", result.Errors);
            roomRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Room>()), Times.Never);
        }
    }

}
