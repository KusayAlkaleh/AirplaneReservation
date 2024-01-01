using System.ComponentModel.DataAnnotations;
using WebProject.Data;

namespace WebProject.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your first name!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your lastname!")]
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please enter your email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password!")]
        public string Password { get; set; }

        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your Date!")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Please select your gender!")]
        public int Gender { get; set; }

		[Required(ErrorMessage = "Please select your class type!")]
		public int ClassType { get; set; }
        public string ProfileImg { get; set; }



       

    }
}
