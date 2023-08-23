using Microsoft.EntityFrameworkCore;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.repository.ClassImplmentation
{
    public class HotelDetails : IHotelDetails
    {
        private ApplicationDbContext dbContext;
        public HotelDetails(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task<List<Room>> GetAvalibleRoom(int id)
        {
            List<Room> roomList = new List<Room>();
            roomList = await dbContext.Rooms.Where(r => r.Hotel_Id == id).ToListAsync();
            return roomList;

        }

        public async Task<City> GetCity(int id)
        {
            int city_id = dbContext.Hotels.FindAsync(id).Result.City_Id;
            return dbContext.Cities.Find(city_id);
        }

        public async Task<Hotel> GetHotelDetail(int id)
        {
            Hotel hotel = new Hotel();
            if (id != null)
            {
                hotel = await dbContext.Hotels.FindAsync(id);
                return hotel;
            }
            else
            {
                return null;
            }

        }

        public async Task<Images> GetImages(int id)
        {
            Images images = new Images();
            if (id != null)
            {
                images = await dbContext.Images.FirstOrDefaultAsync(i => i.Hotel_Id == id);
            }
            else
            {
                images = null;
            }
            return images;

        }

        public async Task<List<Review>> GetReview(int id)
        {
            List<Review> reviews = new List<Review>();
            if (id != null)
            {
                reviews = await dbContext.Reviews.Where(r => r.Hotel_id == id).ToListAsync();
            }
            else
            {
                reviews = null;
            }
            return reviews;
        }

    }
}
