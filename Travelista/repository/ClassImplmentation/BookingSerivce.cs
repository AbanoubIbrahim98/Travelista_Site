using Travelista.Data;

namespace Travelista.repository.ClassImplmentation
{
    public class BookingSerivce
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingSerivce(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteFinishedBookings()
        {
            // Query the database for any bookings with finished stays
            var finishedBookings = _dbContext.Bookings.Where(b => b.Check_Out_Date <= DateTime.Today).ToList();

            // Delete each finished booking
            foreach (var booking in finishedBookings)
            {
                var room = _dbContext.Rooms.FirstOrDefault(r => r.ID == booking.ID);
                _dbContext.Bookings.Remove(booking);
                room.Status = false;
                _dbContext.Update(room);
            }

            // Save changes to the database
            _dbContext.SaveChanges();
        }
    }
}
