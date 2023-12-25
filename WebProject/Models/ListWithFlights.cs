using WebProject.Models.Domain;

namespace WebProject.Models
{
    public class ListWithFlights
    {
        public Flight Flight { get; set; }
        public List<Flight> Flights { get; set; }
        public Dictionary<int, string> AirportNames { get; set; }
    }
}
