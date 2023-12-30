using System.ComponentModel.DataAnnotations;
using WebProject.Models.Domain;

namespace WebProject.Models
{
    public class SearchFlights
    {
        [Required(ErrorMessage = "Please selecet Departure Airport")]
        public string? StartAirport { get; set; }

        [Required(ErrorMessage = "Please selecet Arrival Airport")]
        public string? ArrivalAirport { get; set; }

        [Required(ErrorMessage = "Please selecet Your Flight Date")]
        public DateTime DateOfFlight { get; set; }

        [Required(ErrorMessage = "Please selecet Passenger Type")]
        public int PassengerType { get; set; }

        [Required(ErrorMessage = "Please selecet Gabin Type")]
        public int Gabin { get; set; }


        // Helper Variables
        public List<Airport> Airports { get; set; }
        public List<Flight> Flights { get; set; }
        public Dictionary<int, (string code, string Name, string city)> AirportInfo { get; set; }
    }
}
