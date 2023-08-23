using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.Controllers
{
    public class BookingController : Controller

    {
       
        private readonly ApplicationDbContext dbContext;
        private readonly IBook _book;
        private readonly UserManager<IdentityUser> _userManager;
        public BookingController(ApplicationDbContext dbContext, IBook book, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            _book = book;
            _userManager = userManager;
           
        }
        public async Task<IActionResult> Index(int id)
        {
            var errorMessage = TempData["message"] as string;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
            var Room =  dbContext.Rooms.FirstOrDefault(r => r.ID == id);
            return View(Room);
        }
        [HttpPost]
        public async Task<IActionResult> Book(int room_id, int num_Guest, string roomType, DateTime strart_date, DateTime end_date)
        {
            string user_id = _userManager.GetUserId(User);
            Room room = new();
            room = dbContext.Rooms.FirstOrDefault(room => room.ID == room_id);
            Booking booking1 = new Booking();
            if (ModelState.IsValid)
            {
                if(strart_date.Date > end_date.Date)
				{
                    TempData["message"] = "Please enter the check in date less than the check out date";
                    return RedirectToAction("Index", new {id=room.ID});
				}
				else
				{
                    TimeSpan timeSpan = end_date - strart_date;
                    booking1.User_ID = user_id;
                    booking1.Room_id = room_id;
                    booking1.Num_Guests = num_Guest;
                    booking1.Room_type = roomType;
                    booking1.Check_In_Date = strart_date;
                    booking1.Check_Out_Date = end_date;
                    booking1.Total_Price = dbContext.Rooms.Find(room_id).Price_Per_Night * timeSpan.Days;
                   
                    return View("PaypalPayment",booking1);

                }
                 

            }
            return View();
             

        }
        [Route("Booking/Confirmation")]
        public async Task<IActionResult> Confirmation(string m)
        {
            Booking? booking = JsonConvert.DeserializeObject<Booking>(m);
           
            Booking book = new();
            book = _book.Book_Room(booking.User_ID,(int)booking.Room_id, booking.Num_Guests,
                booking.Room_type, booking.Check_In_Date, booking.Check_Out_Date, booking.Total_Price);
           

            return View("Confrmation");
        }



    }
}
