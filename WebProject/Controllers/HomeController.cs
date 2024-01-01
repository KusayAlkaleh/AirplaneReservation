using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Domain;
using WebProject.Models.DTO;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly DemoDbContext DemoDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, DemoDbContext mvcDemoDbContext, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
			this.DemoDbContext = mvcDemoDbContext;
            _userManager = userManager;
		}

        public IActionResult Index()
        {
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

            // get flight info based on give start, arrival and date flight
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
            // get user id from database
            var idOfUser = await _userManager.GetUserAsync(User);

            // get flight information from database
            var flightInformation = DemoDbContext.Flights.Where(x => x.FlightID == id).Single();

            // get empty seat id from database
            var seatsInformation = await DemoDbContext.Seats.Where(x => x.PlaneId == flightInformation.PlaneId &&
                                                                   x.ReservationStatus == false).ToListAsync();

            var idOfSeat = 0;
            foreach (var item in seatsInformation)
            {
                if(item.ReservationStatus == false)
                {
                    idOfSeat = item.SeatID;
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
            flightInformation.TotalSeats--;
            DemoDbContext.Flights.Update(flightInformation);

            DemoDbContext.Reservation.Add(model);
            await DemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
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