using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.Domain
{
    public class Airport
    {
        [Key]
        public int AirportID { get; set; }

        [Required]
        public string AirportName { get; set; }

        [Required]
        public string CityOfAirport { get; set; }

        [Required]
        public string CountryOfAirport { get; set; }

        // Navigation property
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
