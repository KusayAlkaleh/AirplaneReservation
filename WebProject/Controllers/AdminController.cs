using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{

	[Authorize(Roles = "admin")]
	public class AdminController : Controller
	{
        private readonly DemoDbContext DemoDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(DemoDbContext mvcDemoDbContext, UserManager<ApplicationUser> userManager)
        {
            this.DemoDbContext = mvcDemoDbContext;
			_userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
		{
			var usersInformations = await _userManager.GetUsersInRoleAsync("user");

			// check if is found a users
			if(usersInformations == null)
				return NotFound();

			var usersInList = usersInformations.ToList();

            return View(usersInList);
		}

		[HttpGet]
        public async Task<IActionResult> ViewUsers()
		{
            var usersInformations = await _userManager.GetUsersInRoleAsync("user");

            // check if is found a users
            if (usersInformations == null)
                return NotFound();

            var usersInList = usersInformations.ToList();

            return View(usersInList);
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
