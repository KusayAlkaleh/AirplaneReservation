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
            var newUser = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                ClassType = model.ClassType,
                Password = model.Password,
				BirthDay = model.BirthDay,
			};

			await DemoDbContext.Users.AddAsync(newUser);
			await DemoDbContext.SaveChangesAsync();

			return RedirectToAction("Register");
        }

        public IActionResult ForEach()
        {
            return View();
        }
    }
}
