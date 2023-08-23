using Travelista.Models;

namespace Travelista.repository.Interface
{
    public interface IBook
    {
        Booking Book_Room(string userId, int room_id, int num_Guest, string roomType, DateTime checkIn, DateTime checkOut,decimal total_price);
        void Update_Book(int id, int num_Guest, string roomType, DateTime checkIn, DateTime checkOut);
        void Delete_Book(int id);
        Task<List<Booking>> Get_Book_For_User(string id);

    }
}
