using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.DTO
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Please enter your first name!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your lastname!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your user name!")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter password")]
        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Please enter your Date!")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Please select your gender!")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Please select your class type!")]
        public int ClassType { get; set; }

        public string? ProfileImg { get; set; } 

        public int PhoneNumber { get; set; }

        public string? Role { get; set; }
    }
}
