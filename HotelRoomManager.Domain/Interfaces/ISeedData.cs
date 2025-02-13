using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Domain.Interfaces
{
    public interface ISeedData
    {
        public Task Initialize();
    }
}
