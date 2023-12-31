using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please enter your first name!")]
        public string FirstName { get; set; }

        
        [Required(ErrorMessage = "Please enter your lastname!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your Date!")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Please select your gender!")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Please select your class type!")]
        public int ClassType { get; set; }

        public string ProfileImg { get; set; }

        public int PhoneNumber { get; set; }


        // Navigation property
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
