using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
	public class UserController : Controller
	{
        private readonly DemoDbContext DemoDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(DemoDbContext mvcDemoDbContext, UserManager<ApplicationUser> userManager)
        {
            this.DemoDbContext = mvcDemoDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
		{
            var getUserInformation = await _userManager.GetUserAsync(User);

            if(getUserInformation == null)
                return NotFound(); 


            return View(getUserInformation);
		}

        [HttpPost]
        public async Task<IActionResult> Profile(ApplicationUser model)
        {
            var updatedUser = await _userManager.FindByIdAsync(model.Id);

            if (updatedUser == null)
                return NotFound();


            // get values of GENDER and CLASSTYPE
            int newGenderOfUser = 0, newClassTypeOfUser = 0;
            string gender, classType, newProfileImg;
            gender = Request.Form["Gender"];
            classType = Request.Form["ClassType"];

            // set new value to user [GENDER]
            if (gender == "Male" || gender == "male")
                newGenderOfUser = 1;
            else if (gender == "Female" || gender == "female")
                newGenderOfUser = 2;
            else
                newGenderOfUser = 0;

            // set new value to user [Class Type]
            if (classType == "First" || classType == "first")
                newClassTypeOfUser = 2;
            else if (classType == "Business" || classType == "business")
                newClassTypeOfUser = 1;
            else
                newClassTypeOfUser = 0;

            // set new value to user [Profile image]
            if (newGenderOfUser == 1)
                newProfileImg = "https://bootdey.com/img/Content/avatar/avatar7.png";
            else if (newGenderOfUser == 2)
                newProfileImg = "https://cdn-icons-png.flaticon.com/512/706/706830.png";
            else
                newProfileImg = "https://cdn-icons-png.flaticon.com/512/3143/3143082.png";


            // updating user informations
            updatedUser.FirstName = model.FirstName;
            updatedUser.UserName = model.UserName;
            updatedUser.LastName = model.LastName;
            updatedUser.Email = model.Email;
            updatedUser.PhoneNumber = model.PhoneNumber;
            updatedUser.BirthDay = model.BirthDay;
            updatedUser.Gender = newGenderOfUser;
            updatedUser.ClassType = newClassTypeOfUser;
            updatedUser.ProfileImg = newProfileImg;

            await _userManager.UpdateAsync(updatedUser);
            return View(updatedUser);
        }
    }
}
