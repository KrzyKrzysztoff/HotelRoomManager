using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomManager.Application.Exceptions
{
    public class RoomServiceException(string message, Exception exception) : Exception(message, exception);

    public class RoomNotFoundException(string message) : Exception(message);
}
