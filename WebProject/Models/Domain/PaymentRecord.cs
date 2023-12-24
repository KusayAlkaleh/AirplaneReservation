using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models.Domain
{
    public class PaymentRecord
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z]+[a-zA-Z- ]+$", ErrorMessage ="The Name Isn't Valid")]
        public string Name { get; set; }
        [Required]
        [MinLength(8),MaxLength(19)]
        public string CardNumber { get; set; }
        [Required]
        [Range(2023, 2050)]
        public int ExpYear { get; set; }
        [Required]
        [Range(1,12)]
        public int ExpMonth { get; set; }
    }
}
