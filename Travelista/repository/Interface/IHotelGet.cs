using Travelista.Models;

namespace Travelista.repository.Interface
{
    public interface IHotelGet
    {
        Task<List<Hotel>> GetSomeHotel();
        Task<List<Hotel>> SearchHotel(string City, DateTime StartDate, DateTime EndDate);

    }
}
