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

        public async Task<IActionResult> ViewUsers()
		{
			var users = await DemoDbContext.Users.ToListAsync();

			return View(users);
		}
	}
}
