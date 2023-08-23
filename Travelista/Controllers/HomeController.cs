using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Diagnostics;
using Travelista.Data;
using Travelista.Models;
using Travelista.repository.Interface;
using Travelista.WeatherModel;

namespace Travelista.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IConfiguration configuration;
        private readonly IWeather _iweather;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWeather iweather, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.configuration = configuration;
            _iweather = iweather;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            
            List<Country> countries = new List<Country>();
            countries = dbContext.Countries.ToList();
            ViewBag.city = dbContext.Cities.Where(c => c.Country_Id == 1);
            
            return View(countries);
        }
        public IActionResult Details()
        {
            return View();
        }
      
        [HttpPost]
      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}