using Travelista.Models;

namespace Travelista.repository.Interface
{
    public interface IRoomToBooking
    {
        Task<List<Room>> GetRoomsAvalible(int Hotel_id);
    }
}
