using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.repository.ClassImplmentation
{
    public class BookingRoom : IBook
    {
        ApplicationDbContext DbContext;
        ClaimsPrincipal User;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingRoom(ApplicationDbContext Context, UserManager<IdentityUser> userManager)
        {
            DbContext = Context;
            _userManager = userManager;
        }

        public Booking Book_Room(string userId, int room_id, int num_Guest, string roomType, DateTime checkIn, DateTime checkOut,decimal tatal_price)
        {
            //TimeSpan timeSpan = checkOut - checkIn;
            //DbContext.Rooms.Find(room_id).Price_Per_Night * timeSpan.Days,
            // Create a new Booking object
            var booking = new Booking
            {
                User_ID = userId,
                Room_id = room_id,
                Check_In_Date = checkIn,
                Check_Out_Date = checkOut,
                Room_type = roomType,
                Num_Guests = num_Guest,
                Total_Price = tatal_price


            };
            
            
            var hotel = DbContext.Hotels.Find(DbContext.Rooms.Find(room_id).Hotel_Id);
            if(hotel != null)
            {
                if (roomType == "Single" && hotel.SingleRoom_Number > 0)
                {
                    hotel.SingleRoom_Number -= 1;
                    DbContext.Bookings.Add(booking);
                    DbContext.Update(hotel);

                }
                else if (roomType == "Double" && hotel.DoubleRoom_Number > 0)
                {
                    hotel.DoubleRoom_Number -= 1;
                    DbContext.Bookings.Add(booking);
                    DbContext.Update(hotel);
                }
                else if (roomType == "Triple" && hotel.TripleRoom_Number > 0)
                {
                    hotel.TripleRoom_Number -= 1;
                    DbContext.Bookings.Add(booking);
                }

                else
                {

                }



            }
            DbContext.SaveChanges();
            // Add the booking to the DbContext and save changes

            return booking;
        }

        public void Delete_Book(int id)
        {
            var booking = DbContext.Bookings.Find(id);
            var hotel = DbContext.Hotels.Find(DbContext.Rooms.Find(booking.Room_id).Hotel_Id);
            if (booking != null)
            {
                if (hotel != null)
                {
                    if (booking.Room_type == "Single" && hotel.SingleRoom_Number<=3 )
                    {
                        hotel.SingleRoom_Number += 1;
                        DbContext.Update(hotel);

                    }
                    else if (booking.Room_type == "Double" && hotel.DoubleRoom_Number <= 3)
                    {
                        hotel.DoubleRoom_Number += 1;
                        DbContext.Update(hotel);
                    }
                    else if (booking.Room_type == "Triple" && hotel.TripleRoom_Number <= 3)
                    {
                        hotel.TripleRoom_Number += 1;
                        DbContext.Update(hotel);
                    }

                    else
                    {

                    }

                    DbContext.Bookings.Remove(booking);
                    DbContext.SaveChanges();

                }
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<List<Booking>> Get_Book_For_User(string id)
        {
            List<Booking> bookings = new List<Booking>();
            if (id != null)
            {
                bookings = await DbContext.Bookings.Where(b => b.User_ID == id).ToListAsync();
            }
            else
            {
                bookings = null;
            }
            return bookings;
        }

        public void Update_Book(int id, int num_Guest, string roomType, DateTime checkIn, DateTime checkOut)
        {

            var booking = DbContext.Bookings.Find(id);
            if (booking != null)
            {
                booking.Room_type = roomType;
                booking.Check_In_Date = checkIn;
                booking.Check_Out_Date = checkOut;
                booking.Num_Guests = num_Guest;
                DbContext.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }


    }
}
