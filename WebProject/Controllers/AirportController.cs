using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AirportController : Controller
    {
        private readonly DemoDbContext DemoDbContext;
        public AirportController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Airport> allAirports = await DemoDbContext.Airports.ToListAsync();
            if(allAirports != null)
            {
                ListWithAirport listWithAirport = new ListWithAirport
                {
                    Airports = allAirports
                };
                return View(listWithAirport);
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListWithAirport model)
        {
            if(model.Airport != null)
            {
                await DemoDbContext.Airports.AddAsync(model.Airport);
                await DemoDbContext.SaveChangesAsync();

                return RedirectToAction("Create");
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ListWithAirport model)
        {
            if (model != null && model.Airport != null)
            {
                DemoDbContext.Airports.Update(model.Airport);
                await DemoDbContext.SaveChangesAsync();

                return RedirectToAction("Create");
            }

            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Delete(int airportId)
        {
            var airportInformation = await DemoDbContext.Airports.FindAsync(airportId);

            if(airportInformation != null)
            {
                DemoDbContext.Airports.Remove(airportInformation);
                await DemoDbContext.SaveChangesAsync();

                return RedirectToAction("Create");
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}
