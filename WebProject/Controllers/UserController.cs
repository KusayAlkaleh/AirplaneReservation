using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;

namespace WebProject.Controllers
{
	public class UserController : Controller
	{
        private readonly DemoDbContext DemoDbContext;
        public UserController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
		public async Task<IActionResult> Profile(int id)
		{
            var userFound = await DemoDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userFound == null)
                return RedirectToAction("ViewUsers");

			return View(userFound);
		}

		[HttpPost]
        public IActionResult Profile()
        {

            return View();
        }
    }
}
