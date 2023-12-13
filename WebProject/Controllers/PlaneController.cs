using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class PlaneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
