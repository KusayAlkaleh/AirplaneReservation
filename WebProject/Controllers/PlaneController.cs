using Microsoft.AspNetCore.Mvc;
using WebProject.Data;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plane model)
        {
            if(model != null)
            {
                // Adding to database
                await DemoDbContext.Planes.AddAsync(model);
                await DemoDbContext.SaveChangesAsync();

                // This message for alert Novigation
                ViewBag.msgForAlert = true;

                return RedirectToAction("Create");
            }

            return View();
        }
    }
}
