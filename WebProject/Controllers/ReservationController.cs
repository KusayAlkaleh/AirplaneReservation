using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;

namespace WebProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReservationController : Controller
    {
        private readonly DemoDbContext DemoDbContext;
        public ReservationController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reservationsInformation = await DemoDbContext.Reservation.ToListAsync();

            if( reservationsInformation == null )
            {
                return NotFound();
            }

            return View(reservationsInformation);
        }

        
        public async Task<IActionResult> Delete(int resId)
        {
            var reservationsInformation = await DemoDbContext.Reservation.FirstOrDefaultAsync(x=> x.ReservationsID == resId);

            if (reservationsInformation == null)
            {
                return NotFound();
            }
            DemoDbContext.Reservation.Remove(reservationsInformation);
            await DemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
