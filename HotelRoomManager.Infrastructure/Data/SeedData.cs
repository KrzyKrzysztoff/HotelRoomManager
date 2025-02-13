using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelRoomManager.Domain.Interfaces;
using HotelRoomManager.Domain.Models;
using HotelRoomManager.Infrastructure.Context;

namespace HotelRoomManager.Infrastructure.Data
{
    public class SeedData : ISeedData
    {
        private readonly HotelRoomDbContext _dbContext;

        public SeedData(HotelRoomDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Initialize()
        {
            if (!_dbContext.Rooms.Any())
            {
                List<Room> rooms =
                [
                    new Room()
                    {
                        Description = "Duży pokój",
                        Detail = new AvailabilityDetail { Description = "", Reason = "Zajęty" },
                        Name = "Pokój-001", Size = 5, Status = RoomStatus.Occupied
                    },
                    new Room()
                    {
                        Description = "Średni pokój",
                        Detail = new AvailabilityDetail { Description = "", Reason = "Wolny" },
                        Name = "Pokój-002", Size = 3, Status = RoomStatus.Available
                    },
                    new Room()
                    {
                        Description = "Mały pokój",
                        Detail = new AvailabilityDetail { Description = "", Reason = "Sprzątanie" },
                        Name = "Pokój-003", Size = 2, Status = RoomStatus.Cleaning
                    },
                    new Room()
                    {
                        Description = "Pokój jednosobowy",
                        Detail = new AvailabilityDetail { Description = "Wymiana wykładziny", Reason = "Konserwacja" },
                        Name = "Pokój-004", Size = 1, Status = RoomStatus.Maintenance
                    },
                    new Room()
                    {
                        Description = "Pokój VIP",
                        Detail = new AvailabilityDetail { Description = "Pokój zarezerwowany dla specjalnego gościa.", Reason = "Zamknięty" },
                        Name = "Pokój-005", Size = 2, Status = RoomStatus.ManuallyLocked
                    }
                ];

                await _dbContext.Rooms.AddRangeAsync(rooms);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
