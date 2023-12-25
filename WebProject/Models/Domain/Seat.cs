using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models.Domain
{
    public class Seat
    {
        [Key]
        public int SeatID { get; set; }

        [Required(ErrorMessage = "Please Select The Type of plane!")]
        [ForeignKey("Plane")]
        public int PlaneId { get; set; }

        [Required(ErrorMessage = "Please Enter Seat Number!")]
        public int SeatNumber { get; set; }

        [Required(ErrorMessage = "Please Select Seat Type!")]
        public int? SeatType { get; set; }

        [Required]
        public bool ReservationStatus { get; set ; }

        [Required]
        public int Price { get; set; }


        // Navigation property
        public virtual Plane Plane { get; set; } 
    }
}
