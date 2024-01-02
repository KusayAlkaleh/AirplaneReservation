using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
