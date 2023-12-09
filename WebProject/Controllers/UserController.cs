using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models.Domain;

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
        public async Task<IActionResult> Profile(int userId)
		{
            var userInfo = await DemoDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (userInfo == null)
                return RedirectToAction("ViewUsers");

			return View(userInfo);
		}

		[HttpPost]
        public async Task<IActionResult> Profile(User model)
        {
            var updatedUser = await DemoDbContext.Users.FindAsync(model.Id);

            if (updatedUser != null)
            {
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
                updatedUser.LastName = model.LastName;
                updatedUser.Email = model.Email;
                updatedUser.PhoneNumber = model.PhoneNumber;
                updatedUser.BirthDay = model.BirthDay;
                updatedUser.Gender = newGenderOfUser;
                updatedUser.ClassType = newClassTypeOfUser;
                updatedUser.ProfileImg = newProfileImg;

                await DemoDbContext.SaveChangesAsync();

                //Set boolean alert value for Profile.cshtml
                ViewBag.msgForAlert = true;
                return View(updatedUser);
            }
            
            return View(model);
        }
    }
}
