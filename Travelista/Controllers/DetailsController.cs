using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Travelista.Models;
using Travelista.repository.Interface;

namespace Travelista.Controllers
{
    public class DetailsController : Controller
    {
        private readonly IHotelDetails _hotelDetails;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IReview _review;
        private readonly IRecommendation _recommendation;

        public DetailsController(IHotelDetails hotelDetails, UserManager<IdentityUser> userManager, IReview review, IRecommendation recommendation)
        {
            _hotelDetails = hotelDetails;
            _userManager = userManager;
            _review = review;
            _recommendation = recommendation;
        }

        public async Task<IActionResult> HotelDetail(int id)
        {
            Hotel hotel = new Hotel();
            hotel = await _hotelDetails.GetHotelDetail(id);
            List<Review> reviews = new List<Review>();
            reviews = await _hotelDetails.GetReview(id);
            Images images = new Images();
            images = await _hotelDetails.GetImages(id);
            List<Room> rooms = new List<Room>();
            City city = new();
            city = await _hotelDetails.GetCity(id);
            rooms = await _hotelDetails.GetAvalibleRoom(id);
            if (reviews != null)
            {
                ViewBag.count = reviews.Count;
            }


            return View(hotel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int id, string comment)
        {
            string user_id = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {

                _review.Create(user_id, comment, id);

                return RedirectToAction("HotelDetail", "Details", new { id = id });

            }
            else
            {
                return View("HotelDetail");
            }
        }

        public async Task<IActionResult> BestPlace(int? id)
        {
           
            
                var recom =await _recommendation.GetRecommendations(id);
               
                
                    return View(recom);
                
            }


        }
    
}
