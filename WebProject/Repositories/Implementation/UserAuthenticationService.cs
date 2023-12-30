using Microsoft.AspNetCore.Identity;

namespace WebProject.Repositories.Implementation
{
    public class UserAuthenticationService
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
    }
}
