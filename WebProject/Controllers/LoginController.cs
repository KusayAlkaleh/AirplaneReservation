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

        [HttpGet]
		public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Index(User model)
		{
            var user = DemoDbContext.Users.Where(x => x.Id == model.Id && x.Password == model.Password);

            if( user != null)
            {

            }

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
                // set image for new user
                if (model.Gender == 1)
                    model.ProfileImg = "https://bootdey.com/img/Content/avatar/avatar7.png";
                else if (model.Gender == 2)
                    model.ProfileImg = "https://cdn-icons-png.flaticon.com/512/706/706830.png";
                else
                    model.ProfileImg = "https://cdn-icons-png.flaticon.com/512/3143/3143082.png";

                // Adding new user to database
                await DemoDbContext.Users.AddAsync(model);
				await DemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
			return RedirectToAction("Register");
        }

        public IActionResult ForEach()
        {
            return View();
        }
    }
}
