using Microsoft.AspNetCore.Mvc;
using Travelista.Data;
using Travelista.repository.Interface;
using Travelista.WeatherModel;

namespace Travelista.Controllers
{
    public class SearchController : Controller
    {
        readonly private IHotelGet _hotelGet;
        readonly private IWeather _iweather;
        readonly private ApplicationDbContext dbcontext;
		public SearchController(IHotelGet hotelGet, IWeather weather, ApplicationDbContext dbcontext)
		{
			_hotelGet = hotelGet;
			_iweather = weather;
			this.dbcontext = dbcontext;
		}
		[HttpGet]
        public async Task<IActionResult> Search()
        {
            ViewBag.cities = dbcontext.Cities.Select(c => c.Name).ToList();
            return View();
        }
       

        [HttpPost]
        public async Task<IActionResult> Search(string city ,DateTime strart_date,DateTime end_date)
        {
           
            if (ModelState.IsValid)
            {
                WeatherModel.City tem = new();
                ViewBag.weather = await City(city);
                ViewBag.cities = dbcontext.Cities.Select(c => c.Name).ToList();
                return View(await _hotelGet.SearchHotel(city,strart_date,end_date));
            }
            else
            {
               
            return View();
            }
        }
        public async Task<WeatherModel.City> City(string city)
        {
            RootObject rootObject = await _iweather.GetWeather(city);
            WeatherModel.City ViewModel = new();
            if (rootObject != null)
            {
                ViewModel.Name = rootObject.name;
                ViewModel.Temperature = rootObject.main.temp - 273f;
                ViewModel.Pressure = rootObject.main.pressure;
                ViewModel.Humidity = rootObject.main.humidity;
                ViewModel.Weather = rootObject.weather[0].main;
                ViewModel.Wind = rootObject.wind.speed;
                return ViewModel;

            }
            else
            {
                return null;
            }
           

            
        }
    } 
}
