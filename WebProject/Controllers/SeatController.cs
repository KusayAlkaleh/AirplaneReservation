using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class SeatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
