using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.DTO
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your emai!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password!")]
        public string Password { get; set; }
    }
}
