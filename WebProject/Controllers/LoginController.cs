using Microsoft.AspNetCore.Mvc;
using WebProject.Data;
using WebProject.Models.Domain;
using WebProject.Models.Enums;

namespace WebProject.Controllers
{
    public class LoginController : Controller
    {
		private readonly DemoDbContext DemoDbContext;
		public LoginController(DemoDbContext mvcDemoDbContext)
		{
			this.DemoDbContext = mvcDemoDbContext;
		}

		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public  IActionResult Register()
        {
            return View();
        }


		[HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            
            if ( model != null)
            {
				await DemoDbContext.Users.AddAsync(model);
				await DemoDbContext.SaveChangesAsync();

			}
			return RedirectToAction("Register");
        }

        public IActionResult ForEach()
        {
            return View();
        }
    }
}
