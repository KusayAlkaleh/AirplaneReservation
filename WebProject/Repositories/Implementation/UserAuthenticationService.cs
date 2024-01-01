using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebProject.Models.Domain;
using WebProject.Models.DTO;
using WebProject.Models.Enums;
using WebProject.Repositories.Abstract;

namespace WebProject.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        // Constructs for Identity
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager,
                                         UserManager<ApplicationUser> userManager,
                                         RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Login function
        public async Task<Status> LoginAsync(LoginModel model)
        {
            var status = new Status();

            // Check Field inputs
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Error, invalid email address!";

                return status;
            }

            // Maching user password
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Error, invalid password!";

                return status;
            }


            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);

            // if the result => true
            if (signInResult.Succeeded)
            {
                // Adding role for user
                var rolesOfUser = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };

                foreach (var userRole in rolesOfUser)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Login mission is success
                status.StatusCode = 1;
                status.Message = "Logged in successfully";

                return status;
            }

            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User Locked out!";

                return status;
            }

            else
            {
                status.StatusCode = 0;
                status.Message = "Error, in the Log in function!";

                return status;
            }
        }

        // Register function
        public async Task<Status> RegistrationAsync(RegistrationModel model)
        {
            var status = new Status();

            // Check for user is exist or not
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "The User Already Exists!";

                return status;
            }

            // User not exists
            ApplicationUser newUser = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                UserName = model.UserName,
                LastName = model.LastName, 
                Email = model.Email,
                BirthDay = model.BirthDay,
                Gender = model.Gender,
                ClassType = model.ClassType,
                PhoneNumber = model.PhoneNumber,
                ProfileImg = model.ProfileImg,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newUser, model.Password);

            // Check to result [Logic Error]
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Error, user creation failed!";

                return status;
            }

            // Role management
            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(model.Role))
                await userManager.AddToRoleAsync(newUser, model.Role);

            // Register Mession is success
            status.StatusCode = 1;
            status.Message = "User has registered successfully!";

            return status;
        }

        // Logout function
        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
