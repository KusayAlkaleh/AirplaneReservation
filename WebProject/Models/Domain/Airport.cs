using System.ComponentModel.DataAnnotations;

namespace WebProject.Models.Domain
{
    public class Airport
    {
        [Key]
        public int AirportID { get; set; }

        [Required(ErrorMessage = "Please Enter the Name of Airport!")]
        public string AirportName { get; set; }

        [Required(ErrorMessage = "Please Enter the Name of the Airport City!")]
        public string CityOfAirport { get; set; }

        [Required(ErrorMessage = "Please Enter the Name of the Airport Country!")]
        public string CountryOfAirport { get; set; }

        public string IATACode { get; set; }
    }
}
