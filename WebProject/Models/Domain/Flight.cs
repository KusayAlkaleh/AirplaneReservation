using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models.Domain
{
    public class Flight
    {
        [Key]
        public int FlightID { get; set; }

        [Required]
        [ForeignKey("Airport")]
        public int AirportId { get; set; }

        [Required]
        [ForeignKey("Plane")]
        public int PlaneId { get; set; }

        [Required]
        public string StartingPoint { get; set; }

        [Required]
        public string ArrivingPoint { get; set; }

        [Required]
        public DateTime FlightDate { get; set; }

        [Required]
        public int TotalSeats { get; set; }


        // Navigation property
        public virtual Plane Plane { get; set; }
        public virtual Airport Airport { get; set; }
    }
}
