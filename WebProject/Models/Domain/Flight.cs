using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models.Domain
{
    public class Flight
    {
        [Key]
        public int FlightID { get; set; }

        [Required(ErrorMessage = "Please Slecet the Flight Aircraft!")]
        [ForeignKey("Plane")]
        public int PlaneId { get; set; }

        [Required(ErrorMessage = "Please Enter the Flight number!")]
        public string FlightNumber { get; set; }

        [Required(ErrorMessage = "Please Slecet Departure Airport!")]
        public int StartingPoint { get; set; }

        [Required(ErrorMessage = "Please Slecet Arrival Airport!")]
        public int ArrivingPoint { get; set; }

        [Required(ErrorMessage = "Please Choose a Departure Time!")]
        public DateTime StartingTime { get; set; }

        [Required(ErrorMessage = "Please Choose a Arrival Time!")]
        public DateTime ArrivingTime { get; set; }

        [Required(ErrorMessage = "Please Choose a Departure Date!")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Please Choose a Arrival Date!")]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public int TotalSeats { get; set; }


        // Navigation property
        public virtual Plane Plane { get; set; }
    }
}
