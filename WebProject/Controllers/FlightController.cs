using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                //set AirportId from StartingPoint
                model.Flight.AirportId = int.Parse(model.Flight.StartingPoint);

                await DemoDbContext.Flights.AddAsync(model.Flight);
                await DemoDbContext.SaveChangesAsync();

                return RedirectToAction("Create");
            }

            return RedirectToAction("Admin", "Index");
        }
    }
}
