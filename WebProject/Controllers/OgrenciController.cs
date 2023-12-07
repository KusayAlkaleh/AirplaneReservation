using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class OgrenciController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Kaydet()
        {
            return View();
        }
    }
}
