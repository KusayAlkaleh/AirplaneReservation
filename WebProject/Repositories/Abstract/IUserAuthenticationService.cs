using WebProject.Models.DTO;

namespace WebProject.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task<Status> RegisterationAsync(RegistrationModel model);
        Task LogoutAsync();
    }
}
