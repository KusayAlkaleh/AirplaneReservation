using WebProject.Models.Domain;

namespace WebProject.Models
{
    public class ModelWithFlight
    {
        public Flight Flight { get; set; }
        public List<Plane> Planes { get; set; }
        public List<Airport> Airports { get; set; }
    }
}
