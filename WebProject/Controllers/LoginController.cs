using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProject.Data;
using WebProject.Models.DTO;
using WebProject.Repositories.Abstract;

namespace WebProject.Controllers
{
    public class LoginController : Controller
    {
        // service for checking to user 
        private readonly IUserAuthenticationService _service;
		private readonly DemoDbContext DemoDbContext;

        public LoginController(DemoDbContext mvcDemoDbContext, IUserAuthenticationService service)
		{
			this.DemoDbContext = mvcDemoDbContext;
            this._service = service;
        }

        // Login methods
        [HttpGet]
		public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // check the user inputs
            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        // Registration methods
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
			if (model == null)
				return View(model);

			model.Role = "user";

			// set image for new user
			if (model.Gender == 1)
				model.ProfileImg = "https://bootdey.com/img/Content/avatar/avatar7.png";
			else if (model.Gender == 2)
				model.ProfileImg = "https://cdn-icons-png.flaticon.com/512/706/706830.png";
			else
				model.ProfileImg = "https://cdn-icons-png.flaticon.com/512/3143/3143082.png";


			var result = await _service.RegistrationAsync(model);

			TempData["msg"] = result.Message;
			return RedirectToAction(nameof(Register));
        }


        // Logout method
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
