using Microsoft.AspNetCore.Identity;
using Travelista.Models;

namespace Travelista.repository.Interface
{
    public interface IHotelDetails
    {
        Task<Hotel> GetHotelDetail(int id);
        Task<List<Review>> GetReview(int id);
        Task<Images> GetImages(int id);
        Task<List<Room>> GetAvalibleRoom(int id);
        Task<City> GetCity(int id);



    }
}
