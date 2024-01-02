using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
    public class FlightController : Controller
    {
        private readonly DemoDbContext DemoDbContext;
        public FlightController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ModelWithFlight model = new ModelWithFlight
            {
                Planes = await DemoDbContext.Planes.ToListAsync(),
                Airports = await DemoDbContext.Airports.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelWithFlight model)
        {
            if(model != null && model.Flight != null)
            {
                //set available seat of flight
                model.Flight.TotalSeats = DemoDbContext.Planes.Where(x => x.PlaneID == model.Flight.PlaneId)
                                                    .Select(x => x.ReservedSeats).Single();

                await DemoDbContext.Flights.AddAsync(model.Flight);
                await DemoDbContext.SaveChangesAsync();

                return RedirectToAction("Create");
            }

            return RedirectToAction("Admin", "Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> ShowFlights()
        {

            // Create a dictionary to map airport IDs to their names
            List<Airport> airports = await DemoDbContext.Airports.ToListAsync();

            ListWithFlights model = new ListWithFlights
            {
                AirportNames = airports.ToDictionary(airport => airport.AirportID, airport => airport.AirportName),
                Flights = await DemoDbContext.Flights.ToListAsync()
            };

            return View(model);
        }
    }
}
