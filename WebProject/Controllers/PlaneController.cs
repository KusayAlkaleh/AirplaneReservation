using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class PlaneController : Controller
    {
        private readonly DemoDbContext DemoDbContext;
        public PlaneController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ListObjectPlane model = new ListObjectPlane
            {
                ListOfPlanes = await DemoDbContext.Planes.ToListAsync()
            };
           

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ListObjectPlane model)
        {
            if(model.plane != null)
            {
                //Set value for available seats
                model.plane.AvailableSeats = model.plane.Capacity ?? 0;

                // Adding to database
                await DemoDbContext.Planes.AddAsync(model.plane);
                await DemoDbContext.SaveChangesAsync();

                // This message for alert Novigation
                ViewBag.msgForAlert = true;

                return RedirectToAction("Create");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int planeId)
        {
            var deletedPlane = await DemoDbContext.Planes.FindAsync(planeId);

            if (deletedPlane != null)
            {
                DemoDbContext.Planes.Remove(deletedPlane);
                await DemoDbContext.SaveChangesAsync();

                return RedirectToAction("Create");
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}
