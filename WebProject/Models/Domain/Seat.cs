using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models.Domain
{
    public class Seat
    {
        [Key]
        public int SeatID { get; set; }

        [ForeignKey("Plane")]
        public int PlaneId { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public int SeatType { get; set; }

        [Required]
        public bool ReservationStatus { get; set ; }

        [Required]
        public int Price { get; set; }


        // Navigation property
        public virtual Plane Plane { get; set; }
    }
}
