using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.Domain
{
    public class Plane
    {
        [Key]
        public int PlaneID { get; set; }

        [Required]
        public string PlaneModel { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int YearProduction { get; set; }

        [Required]
        public string OwnerCompany { get; set; }


        // Navigation property
        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
