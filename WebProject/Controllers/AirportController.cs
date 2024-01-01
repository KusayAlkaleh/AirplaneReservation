using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
    public class AirportController : Controller
    {
        private readonly DemoDbContext DemoDbContext;

        private readonly IStringLocalizer<AirportController> _localizer;

        public AirportController(DemoDbContext mvcDemoDbContext, IStringLocalizer<AirportController> localizer)
        {
            this.DemoDbContext = mvcDemoDbContext;
            _localizer = localizer;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
                List<Airport> allAirports = await DemoDbContext.Airports.ToListAsync();
            //ViewData["Havaalanı"] = _localizer["Havaalanı"];
            @ViewData["Airport Mangement"] = _localizer["Airport Mangement"];
            @ViewData["Adjustment"] = _localizer["Adjustment"];
            @ViewData["Country Of Airport"] = _localizer["Country Of Airport"];
            @ViewData["City Of Airport"] = _localizer["City Of Airport"];
            @ViewData["IATA Code"] = _localizer["IATA Code"];
            @ViewData["Airport Name"] = _localizer["Airport Name"];
            @ViewData["Add an Airport"] = _localizer["Add an Airport"];
            //@ViewData["Update & Delete"] = _localizer["Update & Delete"];
            if (allAirports != null)
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
