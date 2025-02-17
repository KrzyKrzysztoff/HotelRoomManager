using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using HotelRoomManager.Infrastructure.Context;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HotelRoomManager.Application.DTOs;
using HotelRoomManager.Domain.Models;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelRoomManager.Tests
{
    public class RoomControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public RoomControllerTests(WebApplicationFactory<Program> factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            _factory = factory;
            _client = _factory.CreateClient(); 
        }

        [Fact]
        public async Task GetAllRooms_ShouldReturnSortedRoomsByNameAsc()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HotelRoomDbContext>();

                context.Rooms.AddRange(
                    new Room { Name = "C Room", Size = 20, Status = RoomStatus.Available },
                    new Room { Name = "A Room", Size = 15, Status = RoomStatus.Occupied },
                    new Room { Name = "B Room", Size = 25, Status = RoomStatus.Available }
                );

                await context.SaveChangesAsync();
            }

            // Act
            var response = await _client.GetAsync("/api/Room?sortBy=NameAsc");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var rooms = await response.Content.ReadFromJsonAsync<List<RoomDto>>();
            rooms.Should().NotBeNull();
            rooms.Should().HaveCountGreaterThan(3);
            rooms.Should().BeInAscendingOrder(r => r.Name);
        }

        [Fact]
        public async Task AddRoom_ShouldReturnCreated_WhenRoomIsAddedSuccessfully()
        {
            // Arrange
            var roomDto = new RoomDto
            {
                Name = "Test Room",
                Size = 20,
                Status = RoomStatus.Available
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Room", roomDto);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HotelRoomDbContext>();
                var room = context.Rooms.FirstOrDefault(r => r.Name == "Test Room");

                room.Should().NotBeNull();
                room.Name.Should().Be("Test Room");
                room.Size.Should().Be(20);
                room.Status.Should().Be(RoomStatus.Available);
            }
        }


    }
}
