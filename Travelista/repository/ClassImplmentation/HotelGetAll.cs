using Microsoft.EntityFrameworkCore;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.repository.ClassImplmentation
{
    public class HotelGetAll : IHotelGet
    {
        ApplicationDbContext DbContext;
        public HotelGetAll(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<List<Hotel>> GetSomeHotel()
        {

            return await DbContext.Hotels.Select(h => h).Take(2).ToListAsync();

        }

        public async Task<List<Hotel>> SearchHotel(string City, DateTime StartDate, DateTime EndDate)
        {
            {
                var city = DbContext.Cities.FirstOrDefault(c => c.Name == City);
                var availableHotels = new List<Hotel>();

                if (city == null)
                {
                    return null;
                }

                else
                {
                    var hotelsInCity = DbContext.Hotels.Where(h => h.City_Id == city.ID).Include(c =>c.images);

                     
                    availableHotels = hotelsInCity.ToList();

                    //foreach (var hotel in hotelsInCity)
                    //{
                    //    var availableRooms = DbContext.Rooms.Where(r => r.Hotel_Id == hotel.ID && r.Status==false);
                    //    var bookingsForHotel = DbContext.Bookings
                    //       .Where(b => b.room.Hotel_Id == hotel.ID && b.Check_In_Date <= StartDate && b.Check_Out_Date >= EndDate)
                    //        .ToList();
                    //    if (availableRooms.Count() > bookingsForHotel.Count())
                    //    {

                    //        availableHotels.Add(hotel);
                    //    }
                    //}
                }

                return availableHotels;
            }

        }
    }
}
