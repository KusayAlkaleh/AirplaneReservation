using Microsoft.AspNetCore.Mvc;
using WebProject.Models.DTO;
using WebProject.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
    public class UserAuthentactionController : Controller
    {
        // service for checking to user 
        private readonly IUserAuthenticationService _service;
        public UserAuthentactionController(IUserAuthenticationService service)
        {
            _service = service;
        }

        // Login methods
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

        // Registeration methods
        public IActionResult Registeration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registeration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Role = "user";
            var result = await _service.RegistrationAsync(model);

            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registeration));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }

        //Method for admin registeration
        public async Task<IActionResult> Reg()
        {
            var model = new RegistrationModel
            {
                UserName = "Admin",
                FirstName = "qusay",
                LastName = "alkaleh",
                Email = "qusay@gmail.com",
                Password = "Admin@12345#",
                ProfileImg = "qusay"
            };

            model.Role = "admin";
            var result = await _service.RegistrationAsync(model);

            return Ok(result);

        }
    }
}
