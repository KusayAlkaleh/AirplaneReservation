using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Domain;
using WebProject.Service;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly DemoDbContext DemoDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LanguageService _localization;

        public HomeController(ILogger<HomeController> logger,
                              DemoDbContext mvcDemoDbContext,
                              UserManager<ApplicationUser> userManager,
                              LanguageService localization)
        {
            _logger = logger;
			this.DemoDbContext = mvcDemoDbContext;
            _userManager = userManager;
            _localization = localization;
		}

        public IActionResult Index()
        {
            ViewBag.welcome = _localization.Getkey("welcome").Value;
            ViewBag.paragraph = _localization.Getkey("paragraph").Value;
            ViewBag.we = _localization.Getkey("we").Value;
            ViewBag.some = _localization.Getkey("some").Value;
            ViewBag.title1 = _localization.Getkey("title1").Value;
            ViewBag.par1 = _localization.Getkey("par1").Value;
            ViewBag.title2 = _localization.Getkey("title2").Value;
            ViewBag.par2 = _localization.Getkey("par2").Value;
            ViewBag.title3 = _localization.Getkey("title3").Value;
            ViewBag.par3 = _localization.Getkey("par3").Value;
            ViewBag.home = _localization.Getkey("home").Value;
            ViewBag.myProfile = _localization.Getkey("myProfile").Value;
            ViewBag.booking = _localization.Getkey("booking").Value;
            ViewBag.service = _localization.Getkey("service").Value;
            ViewBag.about = _localization.Getkey("about").Value;
            ViewBag.contanact = _localization.Getkey("contanact").Value;
            ViewBag.language = _localization.Getkey("language").Value;
            ViewBag.login = _localization.Getkey("login").Value;
            ViewBag.logout = _localization.Getkey("logout").Value;

            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            return View();
        }

        public IActionResult Example()
        {
            ViewBag.Welcome = _localization.Getkey("Welcome").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            return View();
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });

            return Redirect(Request.Headers["Referer"].ToString());
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
			@ViewData["Cabin Type"] = _localizer["Cabin Type"];
			@ViewData["Departing Date"] = _localizer["Departing Date"];
			@ViewData["Flying From"] = _localizer["Flying From"];
			@ViewData["Flying To"] = _localizer["Flying To"];
			@ViewData["Header"] = _localizer["Header"];
			@ViewData["Multi-City"] = _localizer["Multi-City"];
			@ViewData["Number Of Passenger"] = _localizer["Number Of Passenger"];
			@ViewData["One way"] = _localizer["One way"];
			@ViewData["Passenger Selection"] = _localizer["Passenger Selection"];
			@ViewData["Price"] = _localizer["Price"];
			@ViewData["Roundtrip"] = _localizer["Roundtrip"];
			@ViewData["Sentence"] = _localizer["Sentence"];
			@ViewData["Show Flights"] = _localizer["Show Flights"];
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

            // get flight info based on give start, arrival and date flight
            if (allFlights == null)
                return NotFound();
            var  allFlightsInfo = allFlights.Where(x => x.StartingPoint == model.StartAirport &&
                                              x.ArrivingPoint == model.ArrivalAirport &&
                                              x.ArrivalDate == model.DateOfFlight).ToList();


            SearchFlights flightsModel = new SearchFlights {
                Flights = allFlightsInfo,
                FlightsInfo = allFlights.ToDictionary(flight => flight.FlightNumber, flight => flight.TotalSeats),
                AirportInfo = airportsInformation.ToDictionary(airport => airport.AirportID, airport => (
                airport.IATACode,
                airport.AirportName,
                airport.CityOfAirport
                )) 
            };

            return View(flightsModel);
        }


        public async Task<IActionResult> BuySeat(int id)
        {

            Thread.Sleep(5000);

            // get user id from database
            var idOfUser = await _userManager.GetUserAsync(User);

            // get flight information from database
            var flightInformation = await DemoDbContext.Flights.FirstOrDefaultAsync(x => x.FlightID == id);

            if (flightInformation == null)
                return NotFound();

            // get empty seat id from database
            var seatsInformation = await DemoDbContext.Seats.Where(x => x.PlaneId == flightInformation.PlaneId &&
                                                                   x.ReservationStatus == false).ToListAsync();

            var idOfSeat = 0;
            foreach (var item in seatsInformation)
            {
                if(item.ReservationStatus == false)
                {
                    idOfSeat = item.SeatID;
                    item.ReservationStatus = true;

                    DemoDbContext.Seats.Update(item);
                    DemoDbContext.SaveChanges();

                    break;
                }
            }

            // creat a new reveseration
            HttpClient client = new HttpClient();

            string apiUrl = "https://localhost:44381/api/FlightBookingApi";

            Reservation model = new Reservation
            {
                AppUserId = idOfUser.Id,
                FightID = id,
                SeatID = idOfSeat,
                ReservationDate = DateTime.Now,
                PaymentStatus = false,
            };

            // Serialize the Flight object to JSON
            string jsonFlight = JsonConvert.SerializeObject(model);

            // Create a StringContent with the serialized JSON data
            StringContent content = new StringContent(jsonFlight, Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await client.PostAsync(apiUrl, content);


            // update total seat in flight
            if(flightInformation != null)
            {
                flightInformation.TotalSeats--;
                DemoDbContext.Flights.Update(flightInformation);

                DemoDbContext.Reservation.Add(model);
                await DemoDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}