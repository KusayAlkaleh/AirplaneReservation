using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;

namespace WebProject.Controllers
{
	public class AdminController : Controller
	{
        private readonly DemoDbContext DemoDbContext;
        public AdminController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
		{
            var users = await DemoDbContext.Users.ToListAsync();

            return View(users);
		}

		public IActionResult FAQ()
		{
			return View();
		}

		[HttpGet]
        public async Task<IActionResult> ViewUsers()
		{
			var users = await DemoDbContext.Users.ToListAsync();

			return View(users);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int userId)
		{
			var deletedUser = await DemoDbContext.Users.FindAsync(userId);

			if (deletedUser != null)
			{
				DemoDbContext.Users.Remove(deletedUser);
				await DemoDbContext.SaveChangesAsync();

				return RedirectToAction("ViewUsers");
			}

			return RedirectToAction("Index");
		}
	}
}
