using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.Domain
{
    public class Plane
    {
        [Key]
        public int PlaneID { get; set; }

        [Required(ErrorMessage = "Please Enter the Plane Model!")]
        public string PlaneModel { get; set; }

        [Required(ErrorMessage = "Please Enter the Plane Capacity Number!")]
        public int? Capacity { get; set; }

        [Required(ErrorMessage = "Please Enter the Year Production of Plane!")]
        public int? YearProduction { get; set; }

        [Required(ErrorMessage = "Please Select the Owner Company!")]
        public string OwnerCompany { get; set; }

        [Required]
        public int AvailableSeats { get; set; }

        [Required]
        public int ReservedSeats { get; set; }


        // Navigation property
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
