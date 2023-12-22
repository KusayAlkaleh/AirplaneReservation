using WebProject.Models.Domain;

namespace WebProject.Models
{
    public class ListWithAirport
    {
        public Airport Airport { get; set; }
        public List<Airport> Airports { get; set; }
    }
}
