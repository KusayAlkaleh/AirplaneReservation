using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly DemoDbContext DemoDbContext;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(ILogger<HomeController> logger, DemoDbContext mvcDemoDbContext, IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
            _logger = logger;
            this.DemoDbContext = mvcDemoDbContext;
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Index()
        {
            @ViewData["Taste the Creativity"] = _localizer["Taste the Creativity"];
            @ViewData["We Make Awesome Graphic and Web Design"] = _localizer["We Make Awesome Graphic and Web Design"];
            return View();
        }

        public IActionResult Privacy()
        {


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Booking()
        {
            SearchFlights model = new SearchFlights
            {
                Airports = await DemoDbContext.Airports.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ShowFlights(SearchFlights model)
        {
            // get Airports inforamtion to display to user
            List<Airport> airportsInformation = await DemoDbContext.Airports.ToListAsync();


            // get data from [ REST API ] with using HttpClient
            HttpClient client = new HttpClient();

            List<Flight> allFlights = new List<Flight>();
            var response = await client.GetAsync("https://localhost:44381/api/FlightBookingApi");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            allFlights = JsonConvert.DeserializeObject<List<Flight>>(jsonResponse);

            SearchFlights flightsModel = new SearchFlights {
                Flights = allFlights,
                AirportInfo = airportsInformation.ToDictionary(airport => airport.AirportID, airport => (
                airport.IATACode,
                airport.AirportName,
                airport.CityOfAirport
                )) 
            };

            return View(flightsModel);
        }

        public IActionResult Example()
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